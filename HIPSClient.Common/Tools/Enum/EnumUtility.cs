using HIPSClient.Common.Tools.Attributes;

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
  }
}
