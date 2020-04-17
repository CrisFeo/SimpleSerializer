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
      if (WriteNullState(v)) {
        WriteLength(v.Length);
        for (var i = 0; i < v.Length; i++) Int(ref v[i]);
      }
    } else {
      if (ReadNullState()) {
        var length = ReadLength();
        v = new int[length];
        for (var i = 0; i < v.Length; i++) Int(ref v[i]);
      } else {
        v = null;
      }
    }
  }

  public void IntNullable(ref int? v) {
    if (isWriting) {
      if (WriteNullState(v)) {
        var value = v.Value;
        Int(ref value);
      }
    } else {
      if (ReadNullState()) {
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


