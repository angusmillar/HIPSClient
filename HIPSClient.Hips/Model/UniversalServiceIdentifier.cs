using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.Model
{
  public class UniversalServiceIdentifier
  {
    public string LocalCode { get; set; }
    public string LocalCodeDescription { get; set; }
    public string LocalCodeSystemCode { get; set; }
    public string SnomedTermValue { get; set; }
    public string SnomedPreferedTerm { get; set; }
    public string SnomedSystemCode
    {
      get
      {
        return "SCT";
      }
    }
  }
}
