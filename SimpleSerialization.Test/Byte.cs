using System;
using System.Collections.Generic;
using Xunit;
using SimpleSerialization;

using static TestUtils;

public class ByteTests {

  public class TestSerializeByteMsg : Msg {
    public byte myByte;
  }

  public static IEnumerable<object[]> TestSerializeByteData => new[] {
    new[] { new TestSerializeByteMsg { myByte = default }},
    new[] { new TestSerializeByteMsg { myByte = 200 }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeByteData))]
  public static void TestSerializeByte(TestSerializeByteMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeBytesMsg : Msg {
    public byte[] myBytes;
  }

  public static IEnumerable<object[]> TestSerializeBytesData => new[] {
    new[] { new TestSerializeBytesMsg { myBytes = null }},
    new[] { new TestSerializeBytesMsg { myBytes = new byte[] {} }},
    new[] { new TestSerializeBytesMsg { myBytes = new byte[] { default } }},
    new[] { new TestSerializeBytesMsg { myBytes = new byte[] { 45 } }},
    new[] { new TestSerializeBytesMsg { myBytes = new byte[] { 10, 53, 202, 132, 33, 89 } }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeBytesData))]
  public static void TestSerializeBytes(TestSerializeBytesMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeByteNullableMsg : Msg {
    public byte? myByteNullable;
  }

  public static IEnumerable<object[]> TestSerializeByteNullableData => new[] {
    new[] { new TestSerializeByteNullableMsg { myByteNullable = null }},
    new[] { new TestSerializeByteNullableMsg { myByteNullable = default(byte) }},
    new[] { new TestSerializeByteNullableMsg { myByteNullable = 156 }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeByteNullableData))]
  public static void TestSerializeByteNullable(TestSerializeByteNullableMsg input) {
    AssertSerialization(input);
  }

}
