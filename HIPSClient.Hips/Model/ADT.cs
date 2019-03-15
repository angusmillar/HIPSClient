using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIPSClient.Common.Tools.Attributes;
using HIPSClient.Common.Tools.Enum;
using HIPSClient.Common.Tools.String;
using PeterPiper.Hl7.V2.Model;

namespace HIPSClient.Hips.Model
{
  public class ADT : HL7MessageBase
  {
    public Patient Patient { get; set; }
    public HospitalEncounter HospitalEncounter { get; set; }

    public string GetA01Message()
    {
      return CreateADTMessage(HL7EventType.A01).AsStringRaw;
    }

    public string GetA08Message()
    {
      return CreateADTMessage(HL7EventType.A08).AsStringRaw;
    }

    private IMessage CreateADTMessage(HL7EventType EventType)
    {
      IMessage oHL7 = Creator.Message(CreateMSHSegment(HL7MessageType.ADT, EventType));
      oHL7.Add(CreateEVNSegment(EventType, oHL7.Segment("MSH").Field(6).AsString));
      oHL7.Add(CreatePIDSegment(this.Patient));
      oHL7.Add(CreatePV1Segment(this.HospitalEncounter));
      return oHL7;
    }
    
    private ISegment CreateEVNSegment(HL7EventType ADTEvent, string EventDateStamp)
    {
      var oEVN = Creator.Segment("EVN");
      oEVN.Field(1).AsString = ADTEvent.GetLiteral(); 
      oEVN.Field(2).AsString = EventDateStamp;
      return oEVN;
    }
    
  }
}
