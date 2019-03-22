using HIPSClient.Common.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HIPSClient.Common.Tools.Enum
{
  public static class EnumUtility
  {
    public static string GetLiteral(this System.Enum e)
    {
      var attr = e.GetAttributeOnEnum<EnumLiteralAttribute>();

      if (attr != null)
        return attr.Literal;
      else
        return null;
    }

    public static IDictionary<string, Int32> ConvertEnumToDictionary<K>()
    {
      if (typeof(K).BaseType != typeof(System.Enum))
      {
        throw new InvalidCastException();
      }

      return System.Enum.GetValues(typeof(K)).Cast<Int32>().ToDictionary(currentItem => System.Enum.GetName(typeof(K), currentItem));
    }

  }
}
