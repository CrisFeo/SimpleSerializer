using System;
using System.Collections.Generic;
using Xunit;
using SimpleSerialization;

using static TestUtils;

public class UintTests {

  public class TestSerializeUintMsg : Msg {
    public uint myUint;
  }

  public static IEnumerable<object[]> TestSerializeUintData => new[] {
    new[] { new TestSerializeUintMsg { myUint = default }},
    new[] { new TestSerializeUintMsg { myUint = 5000 }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeUintData))]
  public static void TestSerializeUint(TestSerializeUintMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeUintsMsg : Msg {
    public uint[] myUints;
  }

  public static IEnumerable<object[]> TestSerializeUintsData => new[] {
    new[] { new TestSerializeUintsMsg { myUints = null }},
    new[] { new TestSerializeUintsMsg { myUints = new uint[] {} }},
    new[] { new TestSerializeUintsMsg { myUints = new uint[] { default } }},
    new[] { new TestSerializeUintsMsg { myUints = new uint[] { 234 } }},
    new[] { new TestSerializeUintsMsg { myUints = new uint[] { 24553, 2123, 1, 2354, 3235, 1231 } }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeUintsData))]
  public static void TestSerializeUints(TestSerializeUintsMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeUintNullableMsg : Msg {
    public uint? myUintNullable;
  }

  public static IEnumerable<object[]> TestSerializeUintNullableData => new[] {
    new[] { new TestSerializeUintNullableMsg { myUintNullable = null }},
    new[] { new TestSerializeUintNullableMsg { myUintNullable = default(byte) }},
    new[] { new TestSerializeUintNullableMsg { myUintNullable = 34 }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeUintNullableData))]
  public static void TestSerializeUintNullable(TestSerializeUintNullableMsg input) {
    AssertSerialization(input);
  }

}
