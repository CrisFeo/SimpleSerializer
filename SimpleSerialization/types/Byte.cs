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

  public void Byte(ref byte[] v) {
    if (isWriting) {
      WriteLength(v.Length);
      for (var i = 0; i < v.Length; i++) Byte(ref v[i]);
    } else {
      var length = ReadLength();
      v = new byte[length];
      for (var i = 0; i < v.Length; i++) Byte(ref v[i]);
    }
  }

  public void ByteNullable(ref byte? v) {
    if (isWriting) {
      var hasValue = v.HasValue;
      Bool(ref hasValue);
      if (hasValue) {
        var value = v.Value;
        Byte(ref value);
      }
    } else {
      var hasValue = false;
      Bool(ref hasValue);
      if (hasValue) {
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

