using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.Model
{
  public enum IndigenousStatusType
  {
    AboriginalButNotTorresStraitIslanderOrigin,
    TorresStraitIslanderButNotAboriginalOrigin,
    BothAboriginalAndTorresStraitIslanderOrigin,
    NeitherAboriginalNorTorresStraitIslanderOrigin,
    NotStatedInadequatelyDescribed
  }

  public class IndigenousStatus
  {
    public IndigenousStatusType IndigenousStatusType { get; set; }
    public string Code
    {
      get
      {
        switch (IndigenousStatusType)
        {
          case IndigenousStatusType.AboriginalButNotTorresStraitIslanderOrigin:
            return "1";
          case IndigenousStatusType.TorresStraitIslanderButNotAboriginalOrigin:
            return "2";
          case IndigenousStatusType.BothAboriginalAndTorresStraitIslanderOrigin:
            return "3";
          case IndigenousStatusType.NeitherAboriginalNorTorresStraitIslanderOrigin:
            return "4";
          case IndigenousStatusType.NotStatedInadequatelyDescribed:
            return "9";
          default:
            throw new System.ComponentModel.InvalidEnumArgumentException(IndigenousStatusType.ToString(), (int)IndigenousStatusType, typeof(IndigenousStatusType));
        }
      }
    }    
    public string Description
    {
      get
      {
        switch (IndigenousStatusType)
        {
          case IndigenousStatusType.AboriginalButNotTorresStraitIslanderOrigin:
            return "Aboriginal but not Torres Strait Islander origin";
          case IndigenousStatusType.TorresStraitIslanderButNotAboriginalOrigin:
            return "Torres Strait Islander but not Aboriginal origin";
          case IndigenousStatusType.BothAboriginalAndTorresStraitIslanderOrigin:
            return "Both Aboriginal and Torres Strait Islander origin";
          case IndigenousStatusType.NeitherAboriginalNorTorresStraitIslanderOrigin:
            return "Neither Aboriginal nor Torres Strait Islander origin";
          case IndigenousStatusType.NotStatedInadequatelyDescribed:
            return "Not stated/inadequately described";
          default:
            throw new System.ComponentModel.InvalidEnumArgumentException(IndigenousStatusType.ToString(), (int)IndigenousStatusType, typeof(IndigenousStatusType));
        }
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
