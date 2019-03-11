using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterPiper.Hl7.V2.Model;

namespace HIPSClient.Common.HL7Message
{
  public class ADT
  {
    public string A01()
    {
      var oHL7 = Creator.Message("2.4", "ADT", "A01");
      oHL7.Segment("MSH").Field(3).AsString = "ADT";
      oHL7.Segment("MSH").Field(4).AsString = "ANGPATH";
      oHL7.Segment("MSH").Field(5).AsString = "HIB";
      oHL7.Segment("MSH").Field(6).AsString = "HIB";
      oHL7.Segment("MSH").Field(7).Convert.DateTime.SetDateTimeOffset(DateTimeOffset.Now, false);
      oHL7.Segment("MSH").Field(15).AsString = "AL";
      oHL7.Segment("MSH").Field(16).AsString = "NE";
      oHL7.Segment("MSH").Field(17).AsString = "AU";
      oHL7.Segment("MSH").Field(18).AsString = "ASCII";
      oHL7.Segment("MSH").Field(19).AsString = "EN";

      var EVN = Creator.Segment("EVN");
      oHL7.Add(EVN);
      EVN.Field(1).AsString = "A01";
      EVN.Field(1).AsString = oHL7.Segment("MSH").Field(6).AsString;

      var PID = Creator.Segment("PID");
      oHL7.Add(PID);

      //MRN
      var MRN = Creator.Field();
      MRN.Component(1).AsString = "0000020";
      MRN.Component(4).AsString = "ANGPATH";
      MRN.Component(5).AsString = "MR";
      PID.Element(3).Add(MRN);

      //Medicare Number
      var MedicareNumber = Creator.Field();
      MedicareNumber.Component(1).AsString = "4950827451";
      MedicareNumber.Component(4).AsString = "AUSHIC";
      MedicareNumber.Component(5).AsString = "MC";
      PID.Element(3).Add(MedicareNumber);

      //Name
      PID.Field(5).Component(1).AsString = "CURRY";
      PID.Field(5).Component(2).AsString = "ELLENS";
      PID.Field(5).Component(5).AsString = "Ms";
      PID.Field(5).Component(7).AsString = "L";

      //DateTime of Birth
      PID.Field(7).Convert.DateTime.SetDateTimeOffset(new DateTimeOffset(2003, 09, 15, 0, 0, 0, new TimeSpan(10,0,0)), false, PeterPiper.Hl7.V2.Support.Tools.DateTimeSupportTools.DateTimePrecision.Date);

      //Gender
      PID.Field(8).AsString = "F";

      //Race
      PID.Field(10).Component(1).AsString = "1";
      PID.Field(10).Component(2).AsString = "Aboriginal but not Torres Strait Islander origin";
      PID.Field(10).Component(3).AsString = "ISAAC";

      //Address
      PID.Field(11).Component(1).AsString = "";
      PID.Field(11).Component(2).AsString = "9 HERB AVE";
      PID.Field(11).Component(3).AsString = "BROADBEACH";
      PID.Field(11).Component(5).AsString = "4218";
      PID.Field(11).Component(6).AsString = "AUS";
      PID.Field(11).Component(7).AsString = "H";

      //Home Phone
      PID.Field(14).Component(2).AsString = "PRN";
      PID.Field(14).Component(3).AsString = "PH";
      PID.Field(14).Component(7).AsString = "93235615";
      //Work Phone
      PID.Field(15).Component(2).AsString = "WPN";
      PID.Field(15).Component(3).AsString = "CP";
      PID.Field(15).Component(7).AsString = "0414778341";

      var PV1 = Creator.Segment("PV1");
      oHL7.Add(PV1);
      PV1.Field(2).AsString = "I";
      //Ward
      PV1.Field(3).Component(1).AsString = "Ward";
      //Room
      PV1.Field(3).Component(2).AsString = "Room";
      //Bed
      PV1.Field(3).Component(3).AsString = "Bed";
      //Hospital Encounter Number
      PV1.Field(19).AsString = "Visit Number";
      //Admit Date
      PV1.Field(44).Convert.DateTime.SetDateTimeOffset(new DateTimeOffset(2019, 01, 01, 0, 0, 0, new TimeSpan(10, 0, 0)), false, PeterPiper.Hl7.V2.Support.Tools.DateTimeSupportTools.DateTimePrecision.Date);
      //Discharge Date
      PV1.Field(44).Convert.DateTime.SetDateTimeOffset(new DateTimeOffset(2003, 09, 15, 0, 0, 0, new TimeSpan(10, 0, 0)), false, PeterPiper.Hl7.V2.Support.Tools.DateTimeSupportTools.DateTimePrecision.Date);

      return oHL7.AsStringRaw;
    }

  }
}
