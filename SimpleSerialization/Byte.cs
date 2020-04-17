using System;

namespace SimpleSerialization {

public partial class Serializer {

  public void Byte(ref byte v) {
    if (isWriting) {
      buffer[size++] = v;
    } else {
      v = buffer[size++];
    }
  }

  public void Bytes(ref byte[] v) {
    if (isWriting) {
      if (WriteNullState(v)) {
        WriteLength(v.Length);
        for (var i = 0; i < v.Length; i++) Byte(ref v[i]);
      }
    } else {
      if (ReadNullState()) {
        var length = ReadLength();
        v = new byte[length];
        for (var i = 0; i < v.Length; i++) Byte(ref v[i]);
      } else {
        v = null;
      }
    }
  }

  public void ByteNullable(ref byte? v) {
    if (isWriting) {
      if (WriteNullState(v)) {
        var value = v.Value;
        Byte(ref value);
      }
    } else {
      if (ReadNullState()) {
        byte value = default;
        Byte(ref value);
        v = value;
      } else {
        v = null;
      }
    }
  }


}

}

