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

    public static string GetUIDisplay(this System.Enum e)
    {
      var attr = e.GetAttributeOnEnum<EnumUIDisplayAttribute>();

      if (attr != null)
        return attr.Display;
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

    public static IDictionary<string, Int32> ConvertEnumToUIDisplayDictionary<T>()
    {
      if (typeof(T).BaseType != typeof(System.Enum))
      {
        throw new InvalidCastException();
      }

      Dictionary<string, int> Dic = new Dictionary<string, int>();
      int Count = 0;
      foreach (T x in System.Enum.GetValues(typeof(T)))
      {
        string UIDisplay = EnumUtility.GetUIDisplay((x as System.Enum));        
        if (!string.IsNullOrWhiteSpace(UIDisplay))
        {
          Dic.Add(UIDisplay, Count);
        }
        else
        {
          throw new ApplicationException($"The enum {x.ToString()} of type {typeof(T).ToString()} is missing an EnumUIDisplay Attribute");
        }
        Count++;
      }
      return Dic;      
    }
  }
}
