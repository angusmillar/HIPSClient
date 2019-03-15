using HIPSClient.Common.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.Model
{

  public enum ProviderIdentifierType
  {
    [EnumLiteral("HPII")]
    HPII,
    [EnumLiteral("MedicareProviderNumber")]
    MedicareProviderNumber,
    [EnumLiteral("Local")]
    Local,
  }

  public class ProviderIdentifier
  {
    public string Value { get; set; }

    private string _AssigningAuthority;
    public string AssigningAuthority
    {
      get
      {
        switch (Type)
        {
          case ProviderIdentifierType.HPII:
            return Common.HIPS.HipsConfig.HPIIAssigningAuthority;
          case ProviderIdentifierType.MedicareProviderNumber:
            return Common.HIPS.HipsConfig.MedicareProviderNumberAssigningAuthority;
          case ProviderIdentifierType.Local:
            return _AssigningAuthority;
          default:
            throw new System.ComponentModel.InvalidEnumArgumentException(Type.ToString(), (int)Type, typeof(ProviderIdentifierType));
        }
      }
      set
      {
        _AssigningAuthority = value;
      }
    }

    public ProviderIdentifierType Type { get; set; }
  }
}
