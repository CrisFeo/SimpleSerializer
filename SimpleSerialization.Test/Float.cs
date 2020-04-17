using System;
using System.Collections.Generic;
using Xunit;
using SimpleSerialization;

using static TestUtils;

public class FloatTests {

  public class TestSerializeFloatMsg : Msg {
    public float myFloat;
  }

  public static IEnumerable<object[]> TestSerializeFloatData => new[] {
    new[] { new TestSerializeFloatMsg { myFloat = default }},
    new[] { new TestSerializeFloatMsg { myFloat = 123.22f }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeFloatData))]
  public static void TestSerializeFloat(TestSerializeFloatMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeFloatsMsg : Msg {
    public float[] myFloats;
  }

  public static IEnumerable<object[]> TestSerializeFloatsData => new[] {
    new[] { new TestSerializeFloatsMsg { myFloats = null }},
    new[] { new TestSerializeFloatsMsg { myFloats = new float[] {} }},
    new[] { new TestSerializeFloatsMsg { myFloats = new float[] { default } }},
    new[] { new TestSerializeFloatsMsg { myFloats = new float[] { 554.3f } }},
    new[] { new TestSerializeFloatsMsg { myFloats = new float[] { -664.44f, 23.2f, 0.123f, 12455f } }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeFloatsData))]
  public static void TestSerializeFloats(TestSerializeFloatsMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeFloatNullableMsg : Msg {
    public float? myFloatNullable;
  }

  public static IEnumerable<object[]> TestSerializeFloatNullableData => new[] {
    new[] { new TestSerializeFloatNullableMsg { myFloatNullable = null }},
    new[] { new TestSerializeFloatNullableMsg { myFloatNullable = default(byte) }},
    new[] { new TestSerializeFloatNullableMsg { myFloatNullable = 23.3f }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeFloatNullableData))]
  public static void TestSerializeFloatNullable(TestSerializeFloatNullableMsg input) {
    AssertSerialization(input);
  }

}
