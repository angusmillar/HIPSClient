using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterPiper.Hl7.V2.Model;
using PeterPiper.Hl7.V2.Support;
using HIPSClient.HipsTinkerTool.Extention;

namespace HIPSClient.HipsTinkerTool.HL7V2Support
{
  public static class Hl7V2EditorSupport
  {
    public static IMessageDelimiters GetDelimiters(string MSHSegment)
    {
      if (MSHSegment.StartsWith("MSH"))
      {
        var oMessageDelimiters = Creator.MessageDelimiters(
          MSHSegment.Substring(3, 1).ToCharArray()[0],
          MSHSegment.Substring(5, 1).ToCharArray()[0],
          MSHSegment.Substring(4, 1).ToCharArray()[0],
          MSHSegment.Substring(7, 1).ToCharArray()[0],
          MSHSegment.Substring(6, 1).ToCharArray()[0]);                
          return oMessageDelimiters;
      }
      return null;
    }

    public static string GetSchemaInfo(string SegmentPart)
    {
      ISegment oSeg = Creator.Segment(SegmentPart);
      int ElementCount = oSeg.ElementCount;
      int RepeatCount = oSeg.Element(ElementCount).RepeatCount;
      int ComponentCount = oSeg.Element(ElementCount).Repeat(RepeatCount).ComponentCount;
      int SubComponentCount = oSeg.Element(ElementCount).Repeat(RepeatCount).Component(ComponentCount).SubComponentCount;
      return oSeg.Element(ElementCount).Repeat(RepeatCount).Component(ComponentCount).SubComponent(SubComponentCount).PathDetail.PathVerbos;

    }

    public static bool IsHL7DelimeterButNotEscape(string item, IMessageDelimiters oMessageDelimiters)
    {
      if (oMessageDelimiters.Field.ToString() == item ||
        oMessageDelimiters.Component.ToString() == item ||
        oMessageDelimiters.SubComponent.ToString() == item ||
        oMessageDelimiters.Repeat.ToString() == item ||
        oMessageDelimiters.ToString() == item)
        return true;
      else
        return false;
    }

    private static void GetSchemaInfoText(HL7V2PathDetailItems oDetails,
      Dictionary<PeterPiper.Hl7.V2.Schema.Model.VersionsSupported,
      PeterPiper.Hl7.V2.Schema.Support.SchemaSupport> oSchemaSupportDic)
    {
      const string SegmentDescriptionText = "Segment";
      const string FieldDescriptionText = "Field";
      const string ElementUnknownText = "<Unknown Element>";
      const string ComponentUnknownText = "<Unknown Component>";      

      if (oDetails.ElementIndex == 0)
      {
        oDetails.PostionDescriptionBreif = String.Format("{0}", oDetails.SegmentCode);
        oDetails.PostionDescriptionVerbose = String.Format("{0}: {1}", SegmentDescriptionText, oDetails.SegmentCode);
      }
      else if (oDetails.ElementIndex == 1 && oDetails.SegmentCode == PeterPiper.Hl7.V2.Support.Standard.Segments.Msh.Code)
      {        
        oDetails.PostionDescriptionBreif = String.Format("{0}", oDetails.SegmentCode);
        oDetails.PostionDescriptionVerbose = String.Format("{0}: {1}", SegmentDescriptionText, oDetails.SegmentCode);
      }
      else if (oDetails.ElementIndex == 2 && oDetails.SegmentCode == PeterPiper.Hl7.V2.Support.Standard.Segments.Msh.Code)
      {       
        oDetails.PostionDescriptionBreif = String.Format("{0}-{1}", oDetails.SegmentCode, oDetails.ElementIndex);  
        oDetails.PostionDescriptionVerbose = String.Format("{0}: {1}, {2}: {3}", SegmentDescriptionText, oDetails.SegmentCode, FieldDescriptionText, oDetails.ElementIndex);        
      }
      else if (oDetails.MesageVersion == PeterPiper.Hl7.V2.Schema.Model.VersionsSupported.NotSupported)
      {
        oDetails.PostionDescriptionBreif = ElementUnknownText;
        oDetails.ComponentDescription = ComponentUnknownText;
      }
      else
      {
        var oPathDetails = Creator.Segment(oDetails.SegmentText).Element(oDetails.ElementIndex).Repeat(oDetails.RepeatIndex).Component(oDetails.ComponentIndex).SubComponent(oDetails.SubComponentIndex).PathDetail;
        oDetails.PostionDescriptionVerbose = oPathDetails.PathVerbos;
        oDetails.PostionDescriptionBreif = oPathDetails.PathBrief;

        //Get Schema details
        if (oSchemaSupportDic[oDetails.MesageVersion].CurrentSchema.SegmentDictionary.ContainsKey(oDetails.SegmentCode))
        {
          if (oSchemaSupportDic[oDetails.MesageVersion].CurrentSchema.SegmentDictionary[oDetails.SegmentCode].SegmentFieldList.ContainsKey(oDetails.ElementIndex))
          {
            var SegmentFieldSchema = oSchemaSupportDic[oDetails.MesageVersion].CurrentSchema.SegmentDictionary[oDetails.SegmentCode].SegmentFieldList[oDetails.ElementIndex];
            //oDetails.SegmentDescription = //Would love to have a segment description
            oDetails.ElementCanRepeat = SegmentFieldSchema.CanRepeat;
            oDetails.ElementDataTypeCode = SegmentFieldSchema.DataType.Code;
            oDetails.ElementDescription = SegmentFieldSchema.Description;
            oDetails.ElementTableNumber = SegmentFieldSchema.Hl7TableIndex;
            oDetails.ElementMandatory = SegmentFieldSchema.IsMandatory;

            var ElementCompositeSchema = oSchemaSupportDic[oDetails.MesageVersion].CurrentSchema.CompositeList.SingleOrDefault(x => x.Code == oDetails.ElementDataTypeCode);

            if (ElementCompositeSchema != null)
            {
              if (ElementCompositeSchema.CompositeItem.ContainsKey(oDetails.ComponentIndex))
              {
                var CompositeDataType = ElementCompositeSchema.CompositeItem[oDetails.ComponentIndex];
                oDetails.ComponentDescription = CompositeDataType.Description;
                oDetails.ComponentDataTypeCode = CompositeDataType.Type.Code;
                oDetails.ComponentTableNumber = CompositeDataType.Hl7TableIndex;

                ElementCompositeSchema = null;
                ElementCompositeSchema = oSchemaSupportDic[oDetails.MesageVersion].CurrentSchema.CompositeList.SingleOrDefault(x => x.Code == oDetails.ComponentDataTypeCode);

                if (ElementCompositeSchema != null)
                {
                  if (ElementCompositeSchema.CompositeItem.ContainsKey(oDetails.SubComponentIndex))
                  {
                    oDetails.SubComponentDescription = ElementCompositeSchema.CompositeItem[oDetails.SubComponentIndex].Description;
                    oDetails.SubComponentDataTypeCode = ElementCompositeSchema.CompositeItem[oDetails.SubComponentIndex].Type.Code;
                    oDetails.SubComponentTableNumber = ElementCompositeSchema.CompositeItem[oDetails.SubComponentIndex].Hl7TableIndex;
                  }
                }
              }
              else
              {
                oDetails.ComponentDescription = ComponentUnknownText;
              }
            }
            else
            {
              var ElementPrimitiveSchema = oSchemaSupportDic[oDetails.MesageVersion].CurrentSchema.PrimitiveList.SingleOrDefault(x => x.Code == oDetails.ElementDataTypeCode);
              if (ElementPrimitiveSchema != null)
              {
                oDetails.ComponentDataTypeCode = ElementPrimitiveSchema.Code;
                oDetails.ComponentDescription = ElementPrimitiveSchema.Name;
              }
            }      
          }
          else
          {
            oDetails.ElementDescription = ElementUnknownText; 
          }
        }
        else
        {
          oDetails.ElementDescription = ElementUnknownText;          
        }
      }
    }

    public static HL7V2PathDetailItems GetPathDetails(string SegmentLine, 
      int Position,
      IMessage oMessageMSHOnly,
      Dictionary<PeterPiper.Hl7.V2.Schema.Model.VersionsSupported, PeterPiper.Hl7.V2.Schema.Support.SchemaSupport> oSchemaSupportDic)
    {     
      Position--;     
      string LeftPart = SegmentLine.Substring(0, Position);
      string RightPart = SegmentLine.Substring(Position, SegmentLine.Length - Position);      
      
      var Char = string.Empty;
      string LeftElement = string.Empty;
      var counter = LeftPart.Length -1;
      while (Char != oMessageMSHOnly.MessageDelimiters.Field.ToString())
      {        
        Char = LeftPart.Substring(counter, 1);
        LeftElement = Char + LeftElement;
        counter--;
        if (counter < 0)
          break;
      }      
      string RightElement = string.Empty;
      Char = string.Empty;
      counter = 0;
      HL7V2PathDetailItems oDetails = new HL7V2PathDetailItems();
      oDetails.MesageVersion = PeterPiper.Hl7.V2.Schema.Model.Version.GetVersionFromString(oMessageMSHOnly.MessageVersion);      
      oDetails.ItemUnderMouseStructureType = HL7V2PathDetailItems.ItemHL7V2StructureType.Unknown;
      while (Char != oMessageMSHOnly.MessageDelimiters.Field.ToString() && counter < RightPart.Length)
      {
        Char = RightPart.Substring(counter, 1);
        if (Char == oMessageMSHOnly.MessageDelimiters.Component.ToString() && oDetails.ItemUnderMouseStructureType == HL7V2PathDetailItems.ItemHL7V2StructureType.Unknown)
          oDetails.ItemUnderMouseStructureType = HL7V2PathDetailItems.ItemHL7V2StructureType.Component;
        if (Char == oMessageMSHOnly.MessageDelimiters.SubComponent.ToString() && oDetails.ItemUnderMouseStructureType == HL7V2PathDetailItems.ItemHL7V2StructureType.Unknown)
          oDetails.ItemUnderMouseStructureType = HL7V2PathDetailItems.ItemHL7V2StructureType.SubComponent;        
        RightElement = RightElement + Char;
        counter++;        
      }

      var RepeatArray = LeftElement.Split(oMessageMSHOnly.MessageDelimiters.Repeat);
      var ComponentArray = RepeatArray[RepeatArray.Length - 1].Split(oMessageMSHOnly.MessageDelimiters.Component);
      var SubComponentArray = ComponentArray[ComponentArray.Length - 1].Split(oMessageMSHOnly.MessageDelimiters.SubComponent);

      oDetails.SegmentText = SegmentLine;
      oDetails.SegmentCode = SegmentLine.ToUpper().Substring(0, 3);
      oDetails.ElementIndex = LeftPart.Split(oMessageMSHOnly.MessageDelimiters.Field).Length - 1;
      oDetails.RepeatIndex = RepeatArray.Length;
      oDetails.ComponentIndex = ComponentArray.Length;
      oDetails.SubComponentIndex = SubComponentArray.Length;

      if (oDetails.SegmentCode == PeterPiper.Hl7.V2.Support.Standard.Segments.Msh.Code)      
        oDetails.ElementIndex++;
     
      GetSchemaInfoText(oDetails, oSchemaSupportDic);

      return oDetails;     
    }

    public static string FormatToolTipText(HL7V2PathDetailItems oDetails)
    {
      
      StringBuilder sb = new StringBuilder();
      sb.AppendLine(oDetails.PostionDescriptionBreif);
      sb.Append("--");
      for (int i = 0; i < oDetails.PostionDescriptionBreif.Length; i++)
      {
        sb.Append("-");  
      }
      
      if (!String.IsNullOrEmpty(oDetails.ElementDescription))
      {
        sb.AppendLine();
        sb.AppendLine("Field: " + oDetails.ElementIndex.ToString());
        sb.Append("  " + oDetails.ElementDescription);
      }
      if (!String.IsNullOrEmpty(oDetails.ElementDataTypeCode))
      {
        sb.AppendLine();
        sb.Append("  ");
        sb.Append(String.Format("DataType: {0}, Required: {1}, Repeat: {2}", oDetails.ElementDataTypeCode, oDetails.ElementMandatory.ToStringYesNo(), oDetails.ElementCanRepeat.ToStringYesNo()));
        if (oDetails.ElementTableNumber > 0)
        {
          string TableNumber = oDetails.ElementTableNumber.ToString();
          while (TableNumber.Length < 4)
            TableNumber = "0" + TableNumber;
          sb.Append(String.Format(", HL7Table: {0}", TableNumber));
        }
        if (oDetails.ItemUnderMouseStructureType == HL7V2PathDetailItems.ItemHL7V2StructureType.Component || oDetails.ComponentIndex > 1 || oDetails.ItemUnderMouseStructureType == HL7V2PathDetailItems.ItemHL7V2StructureType.SubComponent)
        {
          sb.AppendLine();
          sb.AppendLine("Component: " + oDetails.ComponentIndex.ToString());
          sb.AppendLine("  " + oDetails.ComponentDescription);          
          sb.Append("  ");
          sb.Append(String.Format("DataType: {0}", oDetails.ComponentDataTypeCode));
          if (oDetails.ComponentTableNumber > 0 && !(oDetails.ItemUnderMouseStructureType == HL7V2PathDetailItems.ItemHL7V2StructureType.SubComponent || oDetails.SubComponentIndex > 1))
          {
            var TableNumber = oDetails.ComponentTableNumber.ToString();
            while (TableNumber.Length < 4)
              TableNumber = "0" + TableNumber;
            sb.Append(String.Format(", HL7Table: {0}", TableNumber));
          }
        }
        if (oDetails.ItemUnderMouseStructureType == HL7V2PathDetailItems.ItemHL7V2StructureType.SubComponent || oDetails.SubComponentIndex > 1)
        {
          sb.AppendLine();
          sb.AppendLine("Sub Component: " + oDetails.SubComponentIndex.ToString());
          sb.Append("  " + oDetails.SubComponentDescription);
          sb.AppendLine();
          sb.Append("  ");
          sb.Append(String.Format("DataType: {0}", oDetails.SubComponentDataTypeCode));
          if (oDetails.SubComponentTableNumber > 0)
          {
            var TableNumber = oDetails.SubComponentTableNumber.ToString();
            while (TableNumber.Length < 4)
              TableNumber = "0" + TableNumber;
            sb.Append(String.Format(", HL7Table: {0}", TableNumber));
          }
        }
      }      
      return sb.ToString();
    }
  }
}
