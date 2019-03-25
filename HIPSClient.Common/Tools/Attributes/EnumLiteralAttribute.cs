using System;


namespace HIPSClient.Common.Tools.Attributes
{
  [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
  public sealed class EnumLiteralAttribute : Attribute
  {
    readonly string literal;

    // This is a positional argument
    public EnumLiteralAttribute(string literal)
    {
      this.literal = literal;
    }

    public string Literal
    {
      get { return literal; }
    }
  }

  [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
  public sealed class EnumUIDisplayAttribute : Attribute
  {
    readonly string _Display;
    
    public EnumUIDisplayAttribute(string Display)
    {
      this._Display = Display;
    }

    public string Display
    {
      get { return this._Display; }
    }
  }

}
