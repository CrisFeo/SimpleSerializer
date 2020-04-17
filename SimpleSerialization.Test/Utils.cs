using System;
using System.Collections;
using System.Reflection;
using Xunit;
using DeepEqual.Syntax;
using SimpleSerialization;

static class TestUtils {

  public static void AssertSerialization<T>(T input) where T : Msg, new() {
    byte[] msg;
    {
      var s = new Serializer(1600);
      s.BeginWrite();
      input.Serialize(s);
      msg = new byte[s.Size];
      Buffer.BlockCopy(s.Buffer, 0, msg, 0, s.Size);
    }
    {
      var s = new Serializer(1600);
      s.BeginRead(msg);
      var actual = new T();
      actual.Serialize(s);
      actual.ShouldDeepEqual(input);
    }
  }

}
