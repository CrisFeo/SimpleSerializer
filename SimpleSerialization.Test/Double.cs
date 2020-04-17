using System;
using System.Collections.Generic;
using Xunit;
using SimpleSerialization;

using static TestUtils;

public class DoubleTests {

  public class TestSerializeDoubleMsg : Msg {
    public double myDouble;
  }

  public static IEnumerable<object[]> TestSerializeDoubleData => new[] {
    new[] { new TestSerializeDoubleMsg { myDouble = default }},
    new[] { new TestSerializeDoubleMsg { myDouble = 5654.3 }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeDoubleData))]
  public static void TestSerializeDouble(TestSerializeDoubleMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeDoublesMsg : Msg {
    public double[] myDoubles;
  }

  public static IEnumerable<object[]> TestSerializeDoublesData => new[] {
    new[] { new TestSerializeDoublesMsg { myDoubles = null }},
    new[] { new TestSerializeDoublesMsg { myDoubles = new double[] {} }},
    new[] { new TestSerializeDoublesMsg { myDoubles = new double[] { default } }},
    new[] { new TestSerializeDoublesMsg { myDoubles = new double[] { 0.000023 } }},
    new[] { new TestSerializeDoublesMsg { myDoubles = new double[] { 12.433, 10, 456767.4, -1234.35, 2342345.33 } }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeDoublesData))]
  public static void TestSerializeDoubles(TestSerializeDoublesMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeDoubleNullableMsg : Msg {
    public double? myDoubleNullable;
  }

  public static IEnumerable<object[]> TestSerializeDoubleNullableData => new[] {
    new[] { new TestSerializeDoubleNullableMsg { myDoubleNullable = null }},
    new[] { new TestSerializeDoubleNullableMsg { myDoubleNullable = default(byte) }},
    new[] { new TestSerializeDoubleNullableMsg { myDoubleNullable = 120000.12 }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeDoubleNullableData))]
  public static void TestSerializeDoubleNullable(TestSerializeDoubleNullableMsg input) {
    AssertSerialization(input);
  }

}
