using System;

namespace SimpleSerialization {

public partial class Serializer {

  public void Uint(ref uint v) {
    if (isWriting) {
      buffer[size++] = (byte)((v & 0xFF000000) >> 24);
      buffer[size++] = (byte)((v & 0x00FF0000) >> 16);
      buffer[size++] = (byte)((v & 0x0000FF00) >> 8);
      buffer[size++] = (byte)(v & 0x000000FF);
    } else {
      v = (uint)((buffer[size++]) << 24);
      v |= (uint)((buffer[size++]) << 16);
      v |= (uint)((buffer[size++]) << 8);
      v |= (uint)buffer[size++];
    }
  }

  public void Uints(ref uint[] v) {
    if (isWriting) {
      if (WriteNullState(v)) {
        WriteLength(v.Length);
        for (var i = 0; i < v.Length; i++) Uint(ref v[i]);
      }
    } else {
      if (ReadNullState()) {
        var length = ReadLength();
        v = new uint[length];
        for (var i = 0; i < v.Length; i++) Uint(ref v[i]);
      } else {
        v = null;
      }
    }
  }

  public void UintNullable(ref uint? v) {
    if (isWriting) {
      if (WriteNullState(v)) {
        var value = v.Value;
        Uint(ref value);
      }
    } else {
      if (ReadNullState()) {
        uint value = default;
        Uint(ref value);
        v = value;
      } else {
        v = null;
      }
    }
  }

}

}


