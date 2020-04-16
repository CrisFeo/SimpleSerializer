using System;
using System.Collections.Generic;
using System.Linq;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

public class MsgSerializationWeaver : BaseModuleWeaver {

  public override bool ShouldCleanReference => false;

  public override IEnumerable<string> GetAssembliesForScanning() { yield break; }

  public override void Execute() {
    var serializerType = GetTypeByReference("SimpleSerialization.Serializer");
    if (serializerType == null) return;
    var serializationMethods = serializerType.Methods
      .Where(m => m.ReturnType.FullName == "System.Void")
      .Where(m => m.HasParameters)
      .Where(m => !m.HasGenericParameters)
      .Where(m => m.Parameters.Count() == 1)
      .Where(m => m.Parameters.First().ParameterType.IsByReference);
    WriteInfo($"found serialization methods:\n{string.Join("\n", serializationMethods.Select(m => m.ToString() + " " + new ByReferenceType(m.Parameters.First().ParameterType).ElementType))}");
    var msgType = GetTypeByReference("SimpleSerialization.Msg");
    if (msgType == null) return;
    var baseSerializeMethod = msgType.Methods.FirstOrDefault(m => m.Name == "Serialize");
    if (baseSerializeMethod == null) {
      WriteError($"could not find Serialize method on SimpleSerialization.Msg");
      return;
    }
    var serializeMsgMethod = serializerType.Methods.FirstOrDefault(m => m.Name == "Msg");
    if (serializeMsgMethod == null) {
      WriteError($"could not find Msg serialization method");
      return;
    }
    var serializeMsgsMethod = serializerType.Methods.FirstOrDefault(m => m.Name == "Msgs");
    if (serializeMsgsMethod == null) {
      WriteError($"could not find Msgs serialization method");
      return;
    }
    var msgSubTypes = ModuleDefinition.GetTypes()
      .Where(t => t.IsClass)
      .Where(t => t.BaseType != null)
      .Where(t => t.BaseType.Resolve() == msgType);
    WriteInfo($"found SimpleSerialization.Msg subtypes:\n{string.Join("\n", msgSubTypes.Select(m => m.ToString()))}");
    var autoSubTypes = new List<TypeDefinition>();
    foreach (var t in msgSubTypes) {
      var customSerializeMethod = t.Methods
        .FirstOrDefault(m => m.GetOriginalBaseMethod() == baseSerializeMethod);
      if (customSerializeMethod != null) {
        WriteInfo($"{t} serialized manually using {customSerializeMethod}");
        continue;
      }
      var autoSerializeMethod = new MethodDefinition(
        baseSerializeMethod.Name,
        baseSerializeMethod.Attributes,
        baseSerializeMethod.ReturnType
      );
      autoSerializeMethod.Parameters.Clear();
      foreach (var p in baseSerializeMethod.Parameters) {
        p.ParameterType = ModuleDefinition.ImportReference(p.ParameterType);
        autoSerializeMethod.Parameters.Add(p);
      }
      autoSerializeMethod.Attributes = baseSerializeMethod.Attributes & ~MethodAttributes.NewSlot;
      autoSerializeMethod.Attributes |= MethodAttributes.ReuseSlot;
      autoSerializeMethod.ImplAttributes = baseSerializeMethod.ImplAttributes;
      autoSerializeMethod.SemanticsAttributes = baseSerializeMethod.SemanticsAttributes;
      t.Methods.Add(autoSerializeMethod);
      autoSubTypes.Add(t);
    }
    foreach (var t in autoSubTypes) {
      var serializeMethod = t.Methods.FirstOrDefault(m => m.Name == "Serialize");
      if (serializeMethod == null) {
        WriteError($"{t} did not have a serialization method");
        return;
      }
      var processor = serializeMethod.Body.GetILProcessor();
      var fields = t.Fields
       .Where(f => !f.IsStatic)
       .Where(f => f.IsPublic);
      foreach (var f in fields) {
        var ft = f.FieldType;
        if (ft.Resolve().BaseType.Resolve() == msgType) {
          var m = ft.IsArray ? serializeMsgsMethod : serializeMsgMethod;
          var a = ft.IsArray ? ft.GetElementType() : ft;
          var gim = new GenericInstanceMethod(ModuleDefinition.ImportReference(m));
          gim.GenericArguments.Add(ModuleDefinition.ImportReference(a));
          WriteInfo($"{f} => {gim}");
          processor.Append(processor.Create(OpCodes.Ldarg_1));
          processor.Append(processor.Create(OpCodes.Ldarg_0));
          processor.Append(processor.Create(OpCodes.Ldflda, f));
          processor.Append(processor.Create(OpCodes.Call, gim));
          processor.Append(processor.Create(OpCodes.Nop));
        } else {
          var chosenMethod = serializationMethods.FirstOrDefault(m => {
            var pt = (m.Parameters.First().ParameterType as TypeSpecification).ElementType;
            if (ft.Resolve().IsEnum) {
              if (ft.IsArray != pt.IsArray) return false;
              var ut = ft.Resolve().GetEnumUnderlyingType();
              return ut.FullName == pt.GetElementType().FullName;
            }
            return ft.FullName == pt.FullName;
          });
          if (chosenMethod == null) {
            WriteError($"{f} => ?");
            return;
          }
          WriteInfo($"{f} => {chosenMethod}");
          processor.Append(processor.Create(OpCodes.Ldarg_1));
          processor.Append(processor.Create(OpCodes.Ldarg_0));
          processor.Append(processor.Create(OpCodes.Ldflda, f));
          processor.Append(processor.Create(OpCodes.Callvirt, ModuleDefinition.ImportReference(chosenMethod)));
          processor.Append(processor.Create(OpCodes.Nop));
        }
      }
      processor.Append(processor.Create(OpCodes.Ret));
    }
    // TODO write changes to assembly
  }

  TypeDefinition GetTypeByReference(string name) {
    if (!ModuleDefinition.TryGetTypeReference( name, out var typeRef)) {
      WriteError($"could not find type reference to {name}");
      return null;
    }
    return typeRef.Resolve();
  }

}
