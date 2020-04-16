using System;

namespace SimpleSerialization {

public partial class Serializer {

  public void Int(ref int v) {
    if (isWriting) {
      buffer[size++] = (byte)((v & 0xFF000000) >> 24);
      buffer[size++] = (byte)((v & 0x00FF0000) >> 16);
      buffer[size++] = (byte)((v & 0x0000FF00) >> 8);
      buffer[size++] = (byte)(v & 0x000000FF);
    } else {
      v = (int)((buffer[size++]) << 24);
      v |= (int)((buffer[size++]) << 16);
      v |= (int)((buffer[size++]) << 8);
      v |= (int)buffer[size++];
    }
  }

  public void Ints(ref int[] v) {
    if (isWriting) {
      WriteLength(v.Length);
      for (var i = 0; i < v.Length; i++) Int(ref v[i]);
    } else {
      var length = ReadLength();
      v = new int[length];
      for (var i = 0; i < v.Length; i++) Int(ref v[i]);
    }
  }

  public void IntNullable(ref int? v) {
    if (isWriting) {
      var hasValue = v.HasValue;
      Bool(ref hasValue);
      if (hasValue) {
        var value = v.Value;
        Int(ref value);
      }
    } else {
      var hasValue = false;
      Bool(ref hasValue);
      if (hasValue) {
        int value = default;
        Int(ref value);
        v = value;
      } else {
        v = null;
      }
    }
  }

}

}


