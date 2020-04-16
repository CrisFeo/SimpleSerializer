using System;
using System.Text;
using SimpleSerialization;

class Vec2 : Msg {
  public float x;
  public float y;
  public override string ToString() => Printer.Render(this, p => {
    p.Val("x", x);
    p.Val("y", y);
  });
}

class Vec3 : Msg {
  public float x;
  public float y;
  public float? z;
  public override string ToString() => Printer.Render(this, p => {
    p.Val("x", x);
    p.Val("y", y);
    p.Val("z", z);
  });
  public override void Serialize(Serializer s) {
    s.Float(ref x);
    s.Float(ref y);
    s.FloatNullable(ref z);
  }
}

enum Status : ushort {
  Idle = 0,
  Run = 1,
  Walk = 2,
}

class State : Msg {
  public Vec2 position;
  public Vec2 velocity;
  public Vec2[] foci;
  public Vec3 higherOrder;
  public string[] thoughts;
  public Status status;
  public Status[] subStatuses;
  public string idea;
  public float? mojo;
  public override string ToString() => Printer.Render(this, p => {
    p.Val("position",    position);
    p.Val("velocity",    velocity);
    p.Val("foci",        foci);
    p.Val("higherOrder", higherOrder);
    p.Val("thoughts",    thoughts);
    p.Val("status",      status);
    p.Val("subStatuses", subStatuses);
    p.Val("idea",        idea);
    p.Val("mojo",        mojo);
  });
}

static class Program {

  static void Main(string[] args) {
    string srcStr;
    string dstStr;
    byte[] msg;
    {
      var src = new State{
        position = new Vec2{
          x = 200.54f,
          y = 3500.2f,
        },
        velocity = new Vec2{
          x = 15.2f,
          y = 2.5438f,
        },
        foci = new Vec2[] {
          new Vec2{ x = -50.5f, y=0 },
          new Vec2{ x = 50000.2f, y=-4000 },
        },
        higherOrder = new Vec3{ x = 1, y = 23.2f, z = null },
        thoughts = new string[]{ "I like cheese", "how much is cow" },
        status = Status.Run,
        subStatuses = new Status[] { Status.Run, Status.Walk },
        idea = "self-improvement",
        mojo = 1337.2f,
      };
      srcStr = src.ToString();
      var s = new Serializer(1600);
      s.BeginWrite();
      src.Serialize(s);
      msg = new byte[s.Size];
      Buffer.BlockCopy(s.Buffer, 0, msg, 0, s.Size);
    }
    Console.WriteLine($"src: {srcStr}");
    Console.WriteLine($"bytes: {Hex(msg, 8)}");
    {
      var s = new Serializer(1600);
      s.BeginRead(msg);
      var dst = new State();
      dst.Serialize(s);
      dstStr = dst.ToString();
    }
    Console.WriteLine($"dst: {dstStr}");
    Console.WriteLine($"success: {srcStr == dstStr}");
  }

  static string Hex(byte[] bytes, int width) {
    var sb = new StringBuilder();
    for (var i = 0; i < bytes.Length; i++) {
      sb.Append(bytes[i].ToString("X2").ToUpper());
      sb.Append(" ");
      if ((i+1) % width == 0) sb.AppendLine();
    }
    return sb.ToString();
  }

}
