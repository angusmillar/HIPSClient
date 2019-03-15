using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIPSClient.Hips.Model;
using HIPSClient.Common.Tools.String;

namespace HIPSClient.Hips.PathologyImaging
{
  public class PathologyImagingRequest
  {
    public string HL7ORUMessage { get; set; }
    public ORU ORU { get; set; }
    public string GetHL7Message
    {
      get
      {
        if (HL7ORUMessage.IsSet())
        {
          return HL7ORUMessage;
        }
        else
        {
          return ORU.GetPathologyORUMessage();
        }
      }
    }
  }
}
