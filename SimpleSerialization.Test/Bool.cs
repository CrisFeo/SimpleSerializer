using System;
using System.Collections.Generic;
using Xunit;
using SimpleSerialization;

using static TestUtils;

public class BoolTests {

  public class TestSerializeBoolMsg : Msg {
    public bool myBool;
  }

  public static IEnumerable<object[]> TestSerializeBoolData => new[] {
    new[] { new TestSerializeBoolMsg { myBool = default }},
    new[] { new TestSerializeBoolMsg { myBool = true }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeBoolData))]
  public static void TestSerializeBool(TestSerializeBoolMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeBoolsMsg : Msg {
    public bool[] myBools;
  }

  public static IEnumerable<object[]> TestSerializeBoolsData => new[] {
    new[] { new TestSerializeBoolsMsg { myBools = null }},
    new[] { new TestSerializeBoolsMsg { myBools = new bool[] {} }},
    new[] { new TestSerializeBoolsMsg { myBools = new bool[] { default } }},
    new[] { new TestSerializeBoolsMsg { myBools = new bool[] { true } }},
    new[] { new TestSerializeBoolsMsg { myBools = new bool[] { true, true, true, false, true, true, false, false, true } }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeBoolsData))]
  public static void TestSerializeBools(TestSerializeBoolsMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeBoolNullableMsg : Msg {
    public bool? myBoolNullable;
  }

  public static IEnumerable<object[]> TestSerializeBoolNullableData => new[] {
    new[] { new TestSerializeBoolNullableMsg { myBoolNullable = null }},
    new[] { new TestSerializeBoolNullableMsg { myBoolNullable = default(bool) }},
    new[] { new TestSerializeBoolNullableMsg { myBoolNullable = true }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeBoolNullableData))]
  public static void TestSerializeBoolNullable(TestSerializeBoolNullableMsg input) {
    AssertSerialization(input);
  }

}
