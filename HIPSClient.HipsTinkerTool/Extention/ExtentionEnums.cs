using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;


namespace HIPSClient.HipsTinkerTool.Extention
{
  public static class ExtentionEnums
  {
    public static string GetEnumDescription(this Enum value)
    {
      FieldInfo oFieldInfo = value.GetType().GetField(value.ToString());
      DescriptionAttribute[] attributes = (DescriptionAttribute[]) oFieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
      if (attributes != null && attributes.Length > 0)
        return attributes[0].Description;
      else
        return value.ToString();
    }

    public static T GetValueFromDescription<T>(string Description)
    {
      var type = typeof(T);
      if (!type.IsEnum) throw new InvalidOperationException();
      foreach (var field in type.GetFields())
      {
        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        if (attribute != null)
        {
          if (attribute.Description == Description)
            return (T)field.GetValue(null);
        }
        else
        {
          if (field.Name == Description)
            return (T)field.GetValue(null);
        }
      }
      throw new ArgumentException("Not found.", "Description");
    }
  }

  public static class ExtentionBool
  {
    public static string ToStringYesNo(this bool value)
    {
      if (value)
        return "Yes";
      return "No";
    }
  }
}
