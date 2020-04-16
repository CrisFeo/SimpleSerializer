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
      WriteLength(v.Length);
      for (var i = 0; i < v.Length; i++) Uint(ref v[i]);
    } else {
      var length = ReadLength();
      v = new uint[length];
      for (var i = 0; i < v.Length; i++) Uint(ref v[i]);
    }
  }

  public void UintNullable(ref uint? v) {
    if (isWriting) {
      var hasValue = v.HasValue;
      Bool(ref hasValue);
      if (hasValue) {
        var value = v.Value;
        Uint(ref value);
      }
    } else {
      var hasValue = false;
      Bool(ref hasValue);
      if (hasValue) {
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


