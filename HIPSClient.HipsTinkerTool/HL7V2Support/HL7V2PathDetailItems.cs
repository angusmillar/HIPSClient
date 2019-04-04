using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.HipsTinkerTool.HL7V2Support
{
  public class HL7V2PathDetailItems
  {
    public enum ItemHL7V2StructureType { Unknown, Field, Component, SubComponent };
    public PeterPiper.Hl7.V2.Schema.Model.VersionsSupported MesageVersion { get; set; }
    public string SegmentText { get; set; }    
    public ItemHL7V2StructureType ItemUnderMouseStructureType { get; set; }
    public string SegmentCode { get; set; }
    public int ElementIndex { get; set; }
    public int RepeatIndex { get; set; }
    public int ComponentIndex { get; set; }
    public int SubComponentIndex { get; set; }    

    public string PostionDescriptionVerbose { get; set; }
    public string PostionDescriptionBreif { get; set; }
    
    public string ElementDescription { get; set; }
    public int ElementTableNumber { get; set; }
    public bool ElementCanRepeat { get; set; }
    public bool ElementMandatory { get; set; }
    public string ElementDataTypeCode { get; set; }
    
    public string ComponentDataTypeCode { get; set; }
    public string ComponentDescription { get; set; }
    public int ComponentTableNumber { get; set; }

    public string SubComponentDataTypeCode { get; set; }
    public string SubComponentDescription { get; set; }
    public int SubComponentTableNumber { get; set; }


  }
}
