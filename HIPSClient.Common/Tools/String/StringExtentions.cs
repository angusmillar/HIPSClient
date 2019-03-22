using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Common.Tools.String
{
  public static class StringExtentions
  {
    public static bool IsSet(this string value)
    {
       return !string.IsNullOrWhiteSpace(value);
    }

    public static string RemoveWhitespace(this string value)
    {      
      return new string(value.Where(c => !Char.IsWhiteSpace(c)).ToArray());
    }
  }
}
