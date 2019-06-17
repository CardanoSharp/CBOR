using System;

namespace PeterO.DocGen {
  internal static class TypeNameUtil {
    public static string UndecorateTypeName(string name) {
      var idx = name.IndexOf('`');
      if (idx >= 0) {
        name = name.Substring(0, idx);
      }
      idx = name.IndexOf('[');
      if (idx >= 0) {
        name = name.Substring(0, idx);
      }
      return name;
    }
  }
}
