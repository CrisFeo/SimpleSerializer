using System;
using System.Text;

namespace SimpleSerialization {

public partial class Serializer {

  // Internal vars
  ////////////////////

  Encoding encoding;

  // Public methods
  ////////////////////

  public void String(ref string v) {
    if (isWriting) {
      if (WriteNullState(v)) {
        WriteLength(v.Length);
        if (v.Length > 0) {
          temp = encoding.GetBytes(v);
          for (var i = 0; i < temp.Length; i++) buffer[size++] = temp[i];
        }
      }
    } else {
      if (ReadNullState()) {
        var length = ReadLength();
        if (length > 0) {
          v = encoding.GetString(buffer, size, length * 2);
          size += length * 2;
        } else {
          v = "";
        }
      } else {
        v = null;
      }
    }
  }

  public void Strings(ref string[] v) {
    if (isWriting) {
      if (WriteNullState(v)) {
        WriteLength(v.Length);
        for (var i = 0; i < v.Length; i++) String(ref v[i]);
      }
    } else {
      if (ReadNullState()) {
        var length = ReadLength();
        v = new string[length];
        for (var i = 0; i < v.Length; i++) String(ref v[i]);
      } else {
        v = null;
      }
    }
  }

}

}


