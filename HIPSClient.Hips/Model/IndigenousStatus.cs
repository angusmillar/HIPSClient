using HIPSClient.Common.Tools.Attributes;
using HIPSClient.Common.Tools.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.Model
{
  public enum IndigenousStatusType
  {
    [EnumUIDisplay("Aboriginal but not Torres Strait Islander origin")]
    [EnumLiteral("1")]
    AboriginalButNotTorresStraitIslanderOrigin,
    [EnumUIDisplay("Torres Strait Islander but not Aboriginal origin")]
    [EnumLiteral("2")]
    TorresStraitIslanderButNotAboriginalOrigin,
    [EnumUIDisplay("Both Aboriginal and Torres Strait Islander origin")]
    [EnumLiteral("3")]
    BothAboriginalAndTorresStraitIslanderOrigin,
    [EnumUIDisplay("Neither Aboriginal nor Torres Strait Islander origin")]
    [EnumLiteral("4")]
    NeitherAboriginalNorTorresStraitIslanderOrigin,
    [EnumUIDisplay("Not stated inadequately described")]
    [EnumLiteral("9")]
    NotStatedInadequatelyDescribed
  }

  public class IndigenousStatus
  {
    public IndigenousStatusType IndigenousStatusType { get; set; }
    public string Code
    {
      get
      {
        return IndigenousStatusType.GetLiteral();        
      }
    }    
    public string Description
    {
      get
      {
        return IndigenousStatusType.GetUIDisplay();        
      }
    }
    public string NameOfCodingSystem
    {
      get
      {
        return "ISAAC";
      }
    }
  }
}
