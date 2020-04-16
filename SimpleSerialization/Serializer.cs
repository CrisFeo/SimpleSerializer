using System;
using System.Text;

namespace SimpleSerialization {

public partial class Serializer {

  // Internal vars
  ////////////////////

  int size;
  byte[] buffer;
  bool isWriting;
  byte[] temp;

  // Constructor
  ////////////////////

  public Serializer(int size) {
    buffer = new byte[size];
    encoding = Encoding.GetEncoding("Unicode");
    BeginWrite();
  }

  // Public properties
  ////////////////////

  public bool IsWriting => isWriting;
  public byte[] Buffer => buffer;
  public int Size => size;

  // Public methods
  ////////////////////

  public void BeginWrite() {
    size  = 0;
    boolByteIndex = -1;
    boolBitIndex = 0;
    isWriting = true;
  }

  public void BeginRead(byte[] data) {
    buffer = data;
    size  = 0;
    boolByteIndex = -1;
    boolBitIndex = 0;
    isWriting = false;
  }

  public void WriteLength(int length) {
    temp = BitConverter.GetBytes(Convert.ToUInt16(length));
    buffer[size++] = temp[0];
    buffer[size++] = temp[1];
  }

  public int ReadLength() {
    var length = BitConverter.ToUInt16(buffer, size);
    size += 2;
    return length;
  }

}

}
