using HIPSClient.Common.Tools.String;
using HIPSClient.Hips.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.DatabaseLoader
{
  public class DatabaseLoaderRequest
  {
    public string HL7ADTMessage { get; set; }
    public ADT_A01 ADT_A01 { get; set; }
    public string GetHL7Message
    {
      get
      {
        if (HL7ADTMessage.IsSet())
        {
          return HL7ADTMessage;
        }
        else
        {
          return ADT_A01.GetHL7Message();
        }        
      }
    }
  }
}
