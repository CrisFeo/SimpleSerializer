using System;
using System.Collections.Generic;
using Xunit;
using SimpleSerialization;

using static TestUtils;

public class IntTests {

  public class TestSerializeIntMsg : Msg {
    public int myInt;
  }

  public static IEnumerable<object[]> TestSerializeIntData => new[] {
    new[] { new TestSerializeIntMsg { myInt = default }},
    new[] { new TestSerializeIntMsg { myInt = 1233 }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeIntData))]
  public static void TestSerializeInt(TestSerializeIntMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeIntsMsg : Msg {
    public int[] myInts;
  }

  public static IEnumerable<object[]> TestSerializeIntsData => new[] {
    new[] { new TestSerializeIntsMsg { myInts = null }},
    new[] { new TestSerializeIntsMsg { myInts = new int[] {} }},
    new[] { new TestSerializeIntsMsg { myInts = new int[] { default } }},
    new[] { new TestSerializeIntsMsg { myInts = new int[] { 23 } }},
    new[] { new TestSerializeIntsMsg { myInts = new int[] { 3404, -1233, 34, 123123, 1212 } }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeIntsData))]
  public static void TestSerializeInts(TestSerializeIntsMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeIntNullableMsg : Msg {
    public int? myIntNullable;
  }

  public static IEnumerable<object[]> TestSerializeIntNullableData => new[] {
    new[] { new TestSerializeIntNullableMsg { myIntNullable = null }},
    new[] { new TestSerializeIntNullableMsg { myIntNullable = default(byte) }},
    new[] { new TestSerializeIntNullableMsg { myIntNullable = -235 }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeIntNullableData))]
  public static void TestSerializeIntNullable(TestSerializeIntNullableMsg input) {
    AssertSerialization(input);
  }

}
