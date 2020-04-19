using System;
using System.Collections.Generic;
using Xunit;
using SimpleSerialization;

using static TestUtils;

public class SerializeTests {


  public partial class TestFieldsMsg : Msg {
    public string myPublic;
    private string myPrivate;
    protected string myProtected;
    public string myProperty { get; set; }
  }

  public partial class TestFieldsMsg : Msg {
    public static TestFieldsMsg New(
      string myPublic,
      string myPrivate,
      string myProtected,
      string myProperty
    ) {
      var m = new TestFieldsMsg();
      m.myPublic = myPublic;
      m.myPrivate = myPrivate;
      m.myProtected = myProtected;
      m.myProperty = myProperty;
      return m;
    }
    public (string, string, string, string) Get() {
      return (myPublic, myPrivate, myProtected, myProperty);
    }
  }

  [Fact]
  public static void TestFields() {
    var input = TestFieldsMsg.New(
      "myPublicValue",
      "myPrivateValue",
      "myProtectedValue",
      "myPropertyValue"
    );
    var output = RoundTripSerialize(input);
    var (myPublicInput, myPrivateInput, myProtectedInput, myPropertyInput) = input.Get();
    var (myPublicOutput, myPrivateOutput, myProtectedOutput, myPropertyOutput) = output.Get();
    Assert.Equal(myPublicInput, myPublicOutput);
    Assert.NotEqual(myPrivateInput, myPrivateOutput);
    Assert.NotEqual(myProtectedInput, myProtectedOutput);
    Assert.NotEqual(myPropertyInput, myPropertyOutput);
  }

  public partial class TestCustomSerializerMsg : Msg {
    public string myPublic;
    public string myPrivate;
    public override void Serialize(Serializer s) {
      s.String(ref myPublic);
      s.String(ref myPrivate);
    }
  }

  public partial class TestCustomSerializerMsg : Msg {
    public static TestCustomSerializerMsg New(
      string myPublic,
      string myPrivate
    ) {
      var m = new TestCustomSerializerMsg();
      m.myPublic = myPublic;
      m.myPrivate = myPrivate;
      return m;
    }
    public (string, string) Get() {
      return (myPublic, myPrivate);
    }
  }

  [Fact]
  public static void TestCustomSerializer() {
    var input = TestCustomSerializerMsg.New(
      "myPublicValue",
      "myPrivateValue"
    );
    var output = RoundTripSerialize(input);
    var (myPublicInput, myPrivateInput) = input.Get();
    var (myPublicOutput, myPrivateOutput) = output.Get();
    Assert.Equal(myPublicInput, myPublicOutput);
    Assert.Equal(myPrivateInput, myPrivateOutput);
  }

}

