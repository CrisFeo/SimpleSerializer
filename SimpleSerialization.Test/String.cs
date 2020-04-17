using System;
using System.Collections.Generic;
using Xunit;
using SimpleSerialization;

using static TestUtils;

public class StringTests {

  public class TestSerializeStringMsg : Msg {
    public string myString;
  }

  public static IEnumerable<object[]> TestSerializeStringData => new[] {
    new[] { new TestSerializeStringMsg { myString = default }},
    new[] { new TestSerializeStringMsg { myString = "" }},
    new[] { new TestSerializeStringMsg { myString = "hello" }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeStringData))]
  public static void TestSerializeString(TestSerializeStringMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeStringsMsg : Msg {
    public string[] myStrings;
  }

  public static IEnumerable<object[]> TestSerializeStringsData => new[] {
    new[] { new TestSerializeStringsMsg { myStrings = null }},
    new[] { new TestSerializeStringsMsg { myStrings = new string[] {} }},
    new[] { new TestSerializeStringsMsg { myStrings = new string[] { default } }},
    new[] { new TestSerializeStringsMsg { myStrings = new string[] { "" } }},
    new[] { new TestSerializeStringsMsg { myStrings = new string[] { "blah" } }},
    new[] { new TestSerializeStringsMsg { myStrings = new string[] { "ased", "qwe", "jy", "ffr", "vbkgdbf" } }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeStringsData))]
  public static void TestSerializeStrings(TestSerializeStringsMsg input) {
    AssertSerialization(input);
  }

}
