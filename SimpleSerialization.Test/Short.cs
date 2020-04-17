using System;
using System.Collections.Generic;
using Xunit;
using SimpleSerialization;

using static TestUtils;

public class ShortTests {

  public class TestSerializeShortMsg : Msg {
    public short myShort;
  }

  public static IEnumerable<object[]> TestSerializeShortData => new[] {
    new[] { new TestSerializeShortMsg { myShort = default }},
    new[] { new TestSerializeShortMsg { myShort = 200 }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeShortData))]
  public static void TestSerializeShort(TestSerializeShortMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeShortsMsg : Msg {
    public short[] myShorts;
  }

  public static IEnumerable<object[]> TestSerializeShortsData => new[] {
    new[] { new TestSerializeShortsMsg { myShorts = null }},
    new[] { new TestSerializeShortsMsg { myShorts = new short[] {} }},
    new[] { new TestSerializeShortsMsg { myShorts = new short[] { default } }},
    new[] { new TestSerializeShortsMsg { myShorts = new short[] { 54 } }},
    new[] { new TestSerializeShortsMsg { myShorts = new short[] { -123, 334, 123, 0, -123} }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeShortsData))]
  public static void TestSerializeShorts(TestSerializeShortsMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeShortNullableMsg : Msg {
    public short? myShortNullable;
  }

  public static IEnumerable<object[]> TestSerializeShortNullableData => new[] {
    new[] { new TestSerializeShortNullableMsg { myShortNullable = null }},
    new[] { new TestSerializeShortNullableMsg { myShortNullable = default(byte) }},
    new[] { new TestSerializeShortNullableMsg { myShortNullable = -1234 }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeShortNullableData))]
  public static void TestSerializeShortNullable(TestSerializeShortNullableMsg input) {
    AssertSerialization(input);
  }

}
