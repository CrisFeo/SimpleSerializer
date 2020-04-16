using System;

namespace SimpleSerialization {

public partial class Serializer {

  public void Short(ref short v) {
    if (isWriting) {
      buffer[size++] = (byte)((v & 0xFF00) >> 8);
      buffer[size++] = (byte)(v & 0x00FF);
    } else {
      v = (short)(((short)(buffer[size++])) << 8);
      v |= (short)buffer[size++];
    }
  }

  public void Shorts(ref short[] v) {
    if (isWriting) {
      WriteLength(v.Length);
      for (var i = 0; i < v.Length; i++) Short(ref v[i]);
    } else {
      var length = ReadLength();
      v = new short[length];
      for (var i = 0; i < v.Length; i++) Short(ref v[i]);
    }
  }

  public void ShortNullable(ref short? v) {
    if (isWriting) {
      var hasValue = v.HasValue;
      Bool(ref hasValue);
      if (hasValue) {
        var value = v.Value;
        Short(ref value);
      }
    } else {
      var hasValue = false;
      Bool(ref hasValue);
      if (hasValue) {
        short value = default;
        Short(ref value);
        v = value;
      } else {
        v = null;
      }
    }
  }

}

}


