using System;
using System.Text;

namespace SimpleSerialization {

public class Msg {
  public virtual void Serialize(Serializer s) {}
}

public partial class Serializer {

  public void Msg<T>(ref T v) where T : Msg, new() {
    if (isWriting) {
      var hasValue = v != null;
      Bool(ref hasValue);
      if (hasValue) {
        v.Serialize(this);
      }
    } else {
      var hasValue = false;
      Bool(ref hasValue);
      if (hasValue) {
        v = new T();
        v.Serialize(this);
      } else {
        v = null;
      }
    }
  }

  public void Msgs<T>(ref T[] v) where T : Msg, new() {
    if (isWriting) {
      WriteLength(v.Length);
      for (var i = 0; i < v.Length; i++) Msg(ref v[i]);
    } else {
      var length = ReadLength();
      v = new T[length];
      for (var i = 0; i < v.Length; i++) Msg(ref v[i]);
    }
  }

}

}
