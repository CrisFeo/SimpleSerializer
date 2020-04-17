using System;

namespace SimpleSerialization {

public partial class Serializer {

  // Internal vars
  ////////////////////

  int boolByteIndex;
  int boolBitIndex;

  // Public methods
  ////////////////////

  public void Bool(ref bool v) {
    if (isWriting) {
      if (boolByteIndex == -1 || boolBitIndex == 7) {
        boolByteIndex = size++;
        boolBitIndex = 0;
        buffer[boolByteIndex] = 0;
      }
      if (v) buffer[boolByteIndex] |= (byte)(1 << boolBitIndex);
      boolBitIndex++;
    } else {
      if (boolByteIndex == -1 || boolBitIndex == 7) {
        boolByteIndex = size++;
        boolBitIndex = 0;
      }
      var bit = buffer[boolByteIndex] & (1 << boolBitIndex++);
      v = bit == 0 ? false : true;
    }
  }

  public void Bools(ref bool[] v) {
    if (isWriting) {
      if (WriteNullState(v)) {
        WriteLength(v.Length);
        for (var i = 0; i < v.Length; i++) Bool(ref v[i]);
      }
    } else {
      if (ReadNullState()) {
        var length = ReadLength();
        v = new bool[length];
        for (var i = 0; i < v.Length; i++) Bool(ref v[i]);
      } else {
        v = null;
      }
    }
  }

  public void BoolNullable(ref bool? v) {
    if (isWriting) {
      if (WriteNullState(v)) {
        var value = v.Value;
        Bool(ref value);
      }
    } else {
      if (ReadNullState()) {
        bool value = default;
        Bool(ref value);
        v = value;
      } else {
        v = null;
      }
    }
  }

}

}
