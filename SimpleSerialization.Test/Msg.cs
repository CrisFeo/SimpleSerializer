using System;
using System.Collections.Generic;
using Xunit;
using SimpleSerialization;

using static TestUtils;

public class MsgTests {

  public class NestedMsg : Msg {
    public int myInt;
  }

  public class TestSerializeMsgMsg : Msg {
    public NestedMsg myMsg;
  }

  public static IEnumerable<object[]> TestSerializeMsgData => new[] {
    new[] { new TestSerializeMsgMsg { myMsg = default }},
    new[] { new TestSerializeMsgMsg { myMsg = new NestedMsg { myInt = 2344 } }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeMsgData))]
  public static void TestSerializeMsg(TestSerializeMsgMsg input) {
    AssertSerialization(input);
  }

  public class TestSerializeMsgsMsg : Msg {
    public NestedMsg[] myMsgs;
  }

  public static IEnumerable<object[]> TestSerializeMsgsData => new[] {
    new[] { new TestSerializeMsgsMsg { myMsgs = null }},
    new[] { new TestSerializeMsgsMsg { myMsgs = new NestedMsg[] {} }},
    new[] { new TestSerializeMsgsMsg { myMsgs = new NestedMsg[] { default } }},
    new[] { new TestSerializeMsgsMsg { myMsgs = new NestedMsg[] { new NestedMsg { myInt = 200 } } }},
    new[] { new TestSerializeMsgsMsg { myMsgs = new NestedMsg[] { new NestedMsg { myInt = 11 }, new NestedMsg { myInt = -0 }, new NestedMsg { myInt = 1200 } } }},
  };

  [Theory]
  [MemberData(nameof(TestSerializeMsgsData))]
  public static void TestSerializeMsgs(TestSerializeMsgsMsg input) {
    AssertSerialization(input);
  }

}
