using System;

namespace SimpleSerialization {

public partial class Serializer {

  public void Char(ref char v) {
    if (isWriting) {
      buffer[size++] = (byte)v;
    } else {
      v = (char)buffer[size++];
    }
  }

  public void Chars(ref char[] v) {
    if (isWriting) {
      WriteLength(v.Length);
      for (var i = 0; i < v.Length; i++) Char(ref v[i]);
    } else {
      var length = ReadLength();
      v = new char[length];
      for (var i = 0; i < v.Length; i++) Char(ref v[i]);
    }
  }

  public void CharNullable(ref char? v) {
    if (isWriting) {
      var hasValue = v.HasValue;
      Bool(ref hasValue);
      if (hasValue) {
        var value = v.Value;
        Char(ref value);
      }
    } else {
      var hasValue = false;
      Bool(ref hasValue);
      if (hasValue) {
        char value = default;
        Char(ref value);
        v = value;
      } else {
        v = null;
      }
    }
  }

}

}


