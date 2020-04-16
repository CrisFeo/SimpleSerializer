using System;

namespace SimpleSerialization {

public partial class Serializer {

  public void Ushort(ref ushort v) {
    if (isWriting) {
      buffer[size++] = (byte)((v & 0xFF00) >> 8);
      buffer[size++] = (byte)(v & 0x00FF);
    } else {
      v = (ushort)(((ushort)(buffer[size++])) << 8);
      v |= (ushort)buffer[size++];
    }
  }

  public void Ushorts(ref ushort[] v) {
    if (isWriting) {
      WriteLength(v.Length);
      for (var i = 0; i < v.Length; i++) Ushort(ref v[i]);
    } else {
      var length = ReadLength();
      v = new ushort[length];
      for (var i = 0; i < v.Length; i++) Ushort(ref v[i]);
    }
  }

  public void UshortNullable(ref ushort? v) {
    if (isWriting) {
      var hasValue = v.HasValue;
      Bool(ref hasValue);
      if (hasValue) {
        var value = v.Value;
        Ushort(ref value);
      }
    } else {
      var hasValue = false;
      Bool(ref hasValue);
      if (hasValue) {
        ushort value = default;
        Ushort(ref value);
        v = value;
      } else {
        v = null;
      }
    }
  }

}

}


