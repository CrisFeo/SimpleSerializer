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
      if (WriteNullState(v)) {
        WriteLength(v.Length);
        for (var i = 0; i < v.Length; i++) Char(ref v[i]);
      }
    } else {
      if (ReadNullState()) {
        var length = ReadLength();
        v = new char[length];
        for (var i = 0; i < v.Length; i++) Char(ref v[i]);
      } else {
        v = null;
      }
    }
  }

  public void CharNullable(ref char? v) {
    if (isWriting) {
      if (WriteNullState(v)) {
        var value = v.Value;
        Char(ref value);
      }
    } else {
      if (ReadNullState()) {
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


