using HIPSClient.Common.Tools.Enum;
using HIPSClient.Common.Tools.String;
using PeterPiper.Hl7.V2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.Model
{
  public class ORU : HL7MessageBase
  {
    public Patient Patient { get; set; }
    public HospitalEncounter HospitalEncounter { get; set; }
    public PathologyOrder Order { get; set; }
    public List<PathologyRequest> RequestList { get; set; }
    public PDFReport PDF { get; set; }    

    public string GetPathologyORUMessage()
    {
      IMessage oHL7 = Creator.Message(CreateMSHSegment(HL7MessageType.ORU, HL7EventType.R01));      
      oHL7.Add(CreatePIDSegment(this.Patient));
      oHL7.Add(CreatePV1Segment(this.HospitalEncounter));
      foreach(PathologyRequest Request in this.RequestList)
      {
        oHL7.Add(CreateORCSegment(this.Order, Request));
        oHL7.Add(CreateOBRSegment(this.Order, Request));
      } 
      if (this.PDF != null)
        oHL7.Add(CreateOBXSegment(this.PDF));
      return oHL7.AsStringRaw;    
    }

    protected virtual ISegment CreateORCSegment(PathologyOrder Order, PathologyRequest Request)
    {
      var oORC = Creator.Segment("ORC");
      oORC.Field(1).AsString = "RE";
      //Placer Order number
      oORC.Field(2).AsString = Order.OrderIdentifier;
      //Filler Order number
      oORC.Field(3).AsString = Request.ReportIdentifier;
      //Order status
      oORC.Field(5).AsString = "CM";
      return oORC;
    }

    protected virtual ISegment CreateOBRSegment(PathologyOrder Order, PathologyRequest Request)
    {
      var oOBR = Creator.Segment("OBR");
      oOBR.Field(1).AsString = "RE";
      //Placer Order number
      oOBR.Field(2).AsString = Order.OrderIdentifier;
      //Filler Order number
      oOBR.Field(3).AsString = Request.ReportIdentifier;

      //Report Name Code (Ordered Item)
      if (Request.ReportName != null)
      {
        //Local Code
        if ((Request.ReportName.LocalCode.IsSet() || Request.ReportName.LocalCodeDescription.IsSet()) && Request.ReportName.LocalCodeSystemCode.IsSet())
        {
          oOBR.Field(4).Component(3).AsString = Request.ReportName.LocalCodeSystemCode;
          if (Request.ReportName.LocalCode.IsSet())
            oOBR.Field(4).Component(1).AsString = Request.ReportName.LocalCode;
          if (Request.ReportName.LocalCodeDescription.IsSet())
            oOBR.Field(4).Component(2).AsString = Request.ReportName.LocalCodeDescription;
        }

        //Snomed-CT Term
        if ((Request.ReportName.SnomedTermValue.IsSet() || Request.ReportName.SnomedPreferedTerm.IsSet()) && Request.ReportName.SnomedSystemCode.IsSet())
        {
          oOBR.Field(4).Component(6).AsString = Request.ReportName.SnomedSystemCode;
          if (Request.ReportName.SnomedTermValue.IsSet())
            oOBR.Field(4).Component(4).AsString = Request.ReportName.SnomedTermValue;
          if (Request.ReportName.SnomedPreferedTerm.IsSet())
            oOBR.Field(4).Component(5).AsString = Request.ReportName.SnomedPreferedTerm;
        }
      }

      //Collection DateTime
      oOBR.Field(7).Convert.DateTime.SetDateTimeOffset(Order.CollectionDateTime, true, PeterPiper.Hl7.V2.Support.Tools.DateTimeSupportTools.DateTimePrecision.DateHourMin);

      //Ordering Provider (We really only need the Family name as mandatory!
      if (Order.OrderingProvider != null)
      {
        if (Order.OrderingProvider.Identifer != null)
        {
          oOBR.Field(16).Component(1).AsString = Order.OrderingProvider.Identifer.Value;
          oOBR.Field(16).Component(9).AsString = Order.OrderingProvider.Identifer.AssigningAuthority;
        }
        if (Order.OrderingProvider.Family.IsSet())
          oOBR.Field(16).Component(2).AsString = Order.OrderingProvider.Family;
        if (Order.OrderingProvider.Given.IsSet())
          oOBR.Field(16).Component(3).AsString = Order.OrderingProvider.Given;
        if (Order.OrderingProvider.Title.IsSet())
          oOBR.Field(16).Component(6).AsString = Order.OrderingProvider.Title;
      }

      //Does PCEHR Exist Flag
      if (Order.IsMyHealthRecordDisclosed.HasValue)
      {
        if (Order.IsMyHealthRecordDisclosed.Value)
          oOBR.Field(20).AsString = $"{Common.HIPS.HipsConfig.MyHealthRecordDiscoveredFlagCode}=Y";
        else
          oOBR.Field(20).AsString = $"{Common.HIPS.HipsConfig.MyHealthRecordDiscoveredFlagCode}=N";
      }

      //Reported DateTime      
      oOBR.Field(22).Convert.DateTime.SetDateTimeOffset(Request.ReportedDateTime, true, PeterPiper.Hl7.V2.Support.Tools.DateTimeSupportTools.DateTimePrecision.DateHourMin);

      //Diagnostic Service Sect ID 0074
      if (Request.DepartmentCode.IsSet())
        oOBR.Field(24).AsString = Request.DepartmentCode;

      //Result Status
      oOBR.Field(25).AsString = Request.ReportStatus.GetLiteral();

      //Ordered DateTime      
      oOBR.Field(27).Component(4).Convert.DateTime.SetDateTimeOffset(Order.OrderedDateTime, true, PeterPiper.Hl7.V2.Support.Tools.DateTimeSupportTools.DateTimePrecision.Date);


      //Author Reporting Pathologists
      if (Request.DocumentAuthor != null)
      {
        if (Request.DocumentAuthor.Identifer != null && Request.DocumentAuthor.Identifer.Value.IsSet() && Request.DocumentAuthor.Identifer.AssigningAuthority.IsSet())
        {
          oOBR.Field(32).Component(1).SubComponent(1).AsString = Request.DocumentAuthor.Identifer.Value;
          oOBR.Field(32).Component(1).SubComponent(9).AsString = Request.DocumentAuthor.Identifer.AssigningAuthority;
        }

        if (Request.DocumentAuthor.Family.IsSet())  
          oOBR.Field(32).Component(1).SubComponent(2).AsString = Request.DocumentAuthor.Family;
        if (Request.DocumentAuthor.Given.IsSet())
          oOBR.Field(32).Component(1).SubComponent(3).AsString = Request.DocumentAuthor.Given;
        if (Request.DocumentAuthor.Title.IsSet())
          oOBR.Field(32).Component(1).SubComponent(6).AsString = Request.DocumentAuthor.Title;
      }
      
      

      return oOBR;
    }

    protected virtual ISegment CreateOBXSegment(PDFReport Pdf)
    {
      var oOBX = Creator.Segment("OBX");
      oOBX.Field(1).AsString = "1";
      //Placer Order number
      oOBX.Field(2).AsString = "ED";
      //Filler Order number
      oOBX.Field(3).Component(1).AsString = "PDF";
      oOBX.Field(3).Component(2).AsString = "Display format in PDF";
      oOBX.Field(3).Component(3).AsString = "AUSPDI";

      //The PDF base64 encoded      
      oOBX.Field(5).Component(2).AsString = "application";
      oOBX.Field(5).Component(3).AsString = "pdf";
      oOBX.Field(5).Component(4).AsString = "Base64";
      oOBX.Field(5).Component(5).Convert.Base64.Encode(Pdf.GetPDF());

      //Order status
      oOBX.Field(11).AsString = PDF.ResultStatus.GetLiteral();
      return oOBX;
    }
  }
}
