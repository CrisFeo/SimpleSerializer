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
      WriteLength(v.Length);
      for (var i = 0; i < v.Length; i++) Bool(ref v[i]);
    } else {
      var length = ReadLength();
      v = new bool[length];
      for (var i = 0; i < v.Length; i++) Bool(ref v[i]);
    }
  }

  public void BoolNullable(ref bool? v) {
    if (isWriting) {
      var hasValue = v.HasValue;
      Bool(ref hasValue);
      if (hasValue) {
        var value = v.Value;
        Bool(ref value);
      }
    } else {
      var hasValue = false;
      Bool(ref hasValue);
      if (hasValue) {
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
