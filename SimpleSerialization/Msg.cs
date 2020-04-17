using System;
using System.Text;

namespace SimpleSerialization {

public class Msg {
  public virtual void Serialize(Serializer s) {}
}

public partial class Serializer {

  public void Msg<T>(ref T v) where T : Msg, new() {
    if (isWriting) {
      if (WriteNullState(v)) {
        v.Serialize(this);
      }
    } else {
      if (ReadNullState()) {
        v = new T();
        v.Serialize(this);
      } else {
        v = null;
      }
    }
  }

  public void Msgs<T>(ref T[] v) where T : Msg, new() {
    if (isWriting) {
      if (WriteNullState(v)) {
        WriteLength(v.Length);
        for (var i = 0; i < v.Length; i++) Msg(ref v[i]);
      }
    } else {
      if (ReadNullState()) {
        var length = ReadLength();
        v = new T[length];
        for (var i = 0; i < v.Length; i++) Msg(ref v[i]);
      } else {
        v = null;
      }
    }
  }

}

}
