using System;

namespace SimpleSerialization {

public partial class Serializer {

  public void Double(ref double v) {
    if (isWriting) {
      temp = BitConverter.GetBytes(v);
      buffer[size++] = temp[0];
      buffer[size++] = temp[1];
      buffer[size++] = temp[2];
      buffer[size++] = temp[3];
      buffer[size++] = temp[4];
      buffer[size++] = temp[5];
      buffer[size++] = temp[6];
      buffer[size++] = temp[7];
    } else {
      v = BitConverter.ToDouble(buffer, size);
      size += 8;
    }
  }

  public void Doubles(ref double[] v) {
    if (isWriting) {
      if (WriteNullState(v)) {
        WriteLength(v.Length);
        for (var i = 0; i < v.Length; i++) Double(ref v[i]);
      }
    } else {
      if (ReadNullState()) {
        var length = ReadLength();
        v = new double[length];
        for (var i = 0; i < v.Length; i++) Double(ref v[i]);
      } else {
        v = null;
      }
    }
  }

  public void DoubleNullable(ref double? v) {
    if (isWriting) {
      if (WriteNullState(v)) {
        var value = v.Value;
        Double(ref value);
      }
    } else {
      if (ReadNullState()) {
        double value = default;
        Double(ref value);
        v = value;
      } else {
        v = null;
      }
    }
  }

}

}


