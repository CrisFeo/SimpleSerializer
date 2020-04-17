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
      if (WriteNullState(v)) {
        WriteLength(v.Length);
        for (var i = 0; i < v.Length; i++) Short(ref v[i]);
      }
    } else {
      if (ReadNullState()) {
        var length = ReadLength();
        v = new short[length];
        for (var i = 0; i < v.Length; i++) Short(ref v[i]);
      } else {
        v = null;
      }
    }
  }

  public void ShortNullable(ref short? v) {
    if (isWriting) {
      if (WriteNullState(v)) {
        var value = v.Value;
        Short(ref value);
      }
    } else {
      if (ReadNullState()) {
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


