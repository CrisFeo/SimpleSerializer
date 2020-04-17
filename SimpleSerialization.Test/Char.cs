using System;
using System.Collections.Generic;
using Xunit;
using SimpleSerialization;

using static TestUtils;

public class CharTests {

  public class TestSerializeCharMsg : Msg {
    public char myChar;
  }

  public static IEnumerable<object[]> TestSerializeCharData => new[] {
    new[] { new TestSerializeCharMsg { myChar = default }},
    new[] { new TestSerializeCharMsg { myChar = 'F' }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeCharData))]
  public static void TestSerializeChar(TestSerializeCharMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeCharsMsg : Msg {
    public char[] myChars;
  }

  public static IEnumerable<object[]> TestSerializeCharsData => new[] {
    new[] { new TestSerializeCharsMsg { myChars = null }},
    new[] { new TestSerializeCharsMsg { myChars = new char[] {} }},
    new[] { new TestSerializeCharsMsg { myChars = new char[] { default } }},
    new[] { new TestSerializeCharsMsg { myChars = new char[] { 'D' } }},
    new[] { new TestSerializeCharsMsg { myChars = new char[] { 'E', 's', 'l', ':', '.', '3' } }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeCharsData))]
  public static void TestSerializeChars(TestSerializeCharsMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeCharNullableMsg : Msg {
    public char? myCharNullable;
  }

  public static IEnumerable<object[]> TestSerializeCharNullableData => new[] {
    new[] { new TestSerializeCharNullableMsg { myCharNullable = null }},
    new[] { new TestSerializeCharNullableMsg { myCharNullable = default(char) }},
    new[] { new TestSerializeCharNullableMsg { myCharNullable = '9' }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeCharNullableData))]
  public static void TestSerializeCharNullable(TestSerializeCharNullableMsg input) {
    AssertSerialization(input);
  }

}
