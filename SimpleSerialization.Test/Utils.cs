using System;
using System.Collections;
using System.Reflection;
using Xunit;
using DeepEqual.Syntax;
using SimpleSerialization;

static class TestUtils {

  public static void AssertSerialization<T>(T input) where T : Msg, new() {
    var actual = RoundTripSerialize(input);
    actual.ShouldDeepEqual(input);
  }

  public static T RoundTripSerialize<T>(T input) where T : Msg, new() {
    byte[] msg;
    {
      var s = new Serializer(1600);
      s.BeginWrite();
      input.Serialize(s);
      msg = new byte[s.Size];
      Buffer.BlockCopy(s.Buffer, 0, msg, 0, s.Size);
    }
    var output = new T();
    {
      var s = new Serializer(1600);
      s.BeginRead(msg);
      output.Serialize(s);
    }
    return output;
  }

}
