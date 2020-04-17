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
      if (WriteNullState(v)) {
        WriteLength(v.Length);
        for (var i = 0; i < v.Length; i++) Float(ref v[i]);
      }
    } else {
      if (ReadNullState()) {
        var length = ReadLength();
        v = new float[length];
        for (var i = 0; i < v.Length; i++) Float(ref v[i]);
      } else {
        v = null;
      }
    }
  }

  public void FloatNullable(ref float? v) {
    if (isWriting) {
      if (WriteNullState(v)) {
        var value = v.Value;
        Float(ref value);
      }
    } else {
      if (ReadNullState()) {
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


