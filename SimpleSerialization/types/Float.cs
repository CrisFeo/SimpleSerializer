using System;

namespace SimpleSerialization {

public partial class Serializer {

  public void Float(ref float v) {
    if (isWriting) {
      temp = BitConverter.GetBytes(v);
      buffer[size++] = temp[0];
      buffer[size++] = temp[1];
      buffer[size++] = temp[2];
      buffer[size++] = temp[3];
    } else {
      v = BitConverter.ToSingle(buffer, size);
      size += 4;
    }
  }

  public void Floats(ref float[] v) {
    if (isWriting) {
      WriteLength(v.Length);
      for (var i = 0; i < v.Length; i++) Float(ref v[i]);
    } else {
      var length = ReadLength();
      v = new float[length];
      for (var i = 0; i < v.Length; i++) Float(ref v[i]);
    }
  }

  public void FloatNullable(ref float? v) {
    if (isWriting) {
      var hasValue = v.HasValue;
      Bool(ref hasValue);
      if (hasValue) {
        var value = v.Value;
        Float(ref value);
      }
    } else {
      var hasValue = false;
      Bool(ref hasValue);
      if (hasValue) {
        float value = default;
        Float(ref value);
        v = value;
      } else {
        v = null;
      }
    }
  }

}

}


