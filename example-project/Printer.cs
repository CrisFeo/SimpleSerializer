using System;
using System.Linq;
using System.Text;

class Printer {

  // Internal vars
  ////////////////////

  StringBuilder sb = new StringBuilder();

  // Public methods
  ////////////////////

  public static string Render(object obj, Action<Printer> fn) {
    var p = new Printer();
    p.sb.Append(obj.GetType().Name);
    p.sb.Append("(");
    fn(p);
    p.sb.Append("\n)");
    return p.sb.ToString();
  }

  public void Val(string name, object value) {
    Field(name);
    if (value == null) {
      sb.Append("<null>");
      return;
    }
    sb.Append(Indent(value.ToString(), 0));
  }

  public void Val<T>(string name, T value) where T : Enum {
    Field(name);
    sb.Append(Indent(value.ToString(), 0));
  }

  public void Val(string name, object[] value) {
    Field(name);
    if (value == null) {
      sb.Append("<null>");
      return;
    }
    var strings = value.Select(v => v.ToString()).Select(s => Indent(s, 2));
    sb.Append(Indent("[\n", 2));
    sb.Append(string.Join(Indent(",\n", 2), strings));
    sb.Append(Indent("\n]", 0));
  }

  public void Val<T>(string name, T[] value) where T : Enum {
    Field(name);
    var strings = value.Select(v => v.ToString()).Select(s => Indent(s, 2));
    sb.Append(Indent("[\n", 2));
    sb.Append(string.Join(Indent(",\n", 2), strings));
    sb.Append(Indent("\n]", 0));
  }

  // Internal methods
  ////////////////////

  void Field(string name) {
    sb.Append("\n");
    sb.Append(new string(' ', 2));
    sb.Append(name);
    sb.Append(": ");
  }

  string Indent(string value, int amount) {
    return value.Replace("\n", "\n" +new string(' ', amount + 2));
  }

}

