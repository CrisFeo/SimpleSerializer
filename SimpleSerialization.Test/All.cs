using System;
using System.Collections.Generic;
using Xunit;
using SimpleSerialization;

using static TestUtils;

public class AllTests {

  public class NestedMsg : Msg {
    public int myInt;
  }

  public class TestSerializeAllMsg : Msg {
    public bool myBool;
    public bool[] myBools;
    public bool? myBoolNullable;
    public byte myByte;
    public byte[] myBytes;
    public byte? myByteNullable;
    public char myChar;
    public char[] myChars;
    public char? myCharNullable;
    public double myDouble;
    public double[] myDoubles;
    public double? myDoubleNullable;
    public float myFloat;
    public float[] myFloats;
    public float? myFloatNullable;
    public int myInt;
    public int[] myInts;
    public int? myIntNullable;
    public short myShort;
    public short[] myShorts;
    public short? myShortNullable;
    public string myString;
    public string[] myStrings;
    public uint myUint;
    public uint[] myUints;
    public uint? myUintNullable;
    public ushort myUshort;
    public ushort[] myUshorts;
    public ushort? myUshortNullable;
    public NestedMsg myNestedMsg;
    public NestedMsg[] myNestedMsgs;
    public TestSerializeAllMsg myTestSerializeAllMsg;
  }

  public static IEnumerable<object[]> TestSerializeAllData => new[] {
    new[] { new TestSerializeAllMsg() },
    new[] { new TestSerializeAllMsg {
      myBool = true,
      myBools = new bool[] { true, false, true },
      myBoolNullable = true,
      myByte = 32,
      myBytes = new byte[] { 233, 43, 11 },
      myByteNullable = 43,
      myChar = '2',
      myChars = new char[] { 'a', '-', 'B' },
      myCharNullable = 'H',
      myDouble = -45566.67754,
      myDoubles = new double[] { 12.3, -134, 455543434.3 },
      myDoubleNullable = 3444.2,
      myFloat = 345.2f,
      myFloats = new float[] { -32434f, 112f, 0.44f },
      myFloatNullable = -5543434.452f,
      myInt = 1234,
      myInts = new int[] { -32, 1233, 4556},
      myIntNullable = 54,
      myShort = 24,
      myShorts = new short[] { -42, 223, 45 },
      myShortNullable = -755,
      myString = "hello",
      myStrings = new string[] { "hi", "there", "people!" },
      myUint = 53,
      myUints = new uint[] { 12, 3425, 1332, 3443 },
      myUintNullable = 544,
      myUshort = 32,
      myUshorts = new ushort[] { 43, 1234, 22 },
      myUshortNullable = 2,
      myNestedMsg = new NestedMsg { myInt = -32 },
      myNestedMsgs = new NestedMsg[] { new NestedMsg { myInt = 2 }, new NestedMsg { myInt = 200 }, new NestedMsg { myInt = -432 } },
      myTestSerializeAllMsg = new TestSerializeAllMsg {
        myUint = 21,
        myInts = new int[] { 45, -233, 2332343 },
      },
    }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeAllData))]
  public static void TestSerializeAll(TestSerializeAllMsg input) {
    AssertSerialization(input);
  }

}
