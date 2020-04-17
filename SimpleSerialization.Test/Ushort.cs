using System;
using System.Collections.Generic;
using Xunit;
using SimpleSerialization;

using static TestUtils;

public class UshortTests {

  public class TestSerializeUshortMsg : Msg {
    public ushort myUshort;
  }

  public static IEnumerable<object[]> TestSerializeUshortData => new[] {
    new[] { new TestSerializeUshortMsg { myUshort = default }},
    new[] { new TestSerializeUshortMsg { myUshort = 200 }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeUshortData))]
  public static void TestSerializeUshort(TestSerializeUshortMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeUshortsMsg : Msg {
    public ushort[] myUshorts;
  }

  public static IEnumerable<object[]> TestSerializeUshortsData => new[] {
    new[] { new TestSerializeUshortsMsg { myUshorts = null }},
    new[] { new TestSerializeUshortsMsg { myUshorts = new ushort[] {} }},
    new[] { new TestSerializeUshortsMsg { myUshorts = new ushort[] { default } }},
    new[] { new TestSerializeUshortsMsg { myUshorts = new ushort[] { 123 } }},
    new[] { new TestSerializeUshortsMsg { myUshorts = new ushort[] { 11, 56, 189, 110, 34, 90, 1 } }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeUshortsData))]
  public static void TestSerializeUshorts(TestSerializeUshortsMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeUshortNullableMsg : Msg {
    public ushort? myUshortNullable;
  }

  public static IEnumerable<object[]> TestSerializeUshortNullableData => new[] {
    new[] { new TestSerializeUshortNullableMsg { myUshortNullable = null }},
    new[] { new TestSerializeUshortNullableMsg { myUshortNullable = default(byte) }},
    new[] { new TestSerializeUshortNullableMsg { myUshortNullable = 244 }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeUshortNullableData))]
  public static void TestSerializeUshortNullable(TestSerializeUshortNullableMsg input) {
    AssertSerialization(input);
  }

}
