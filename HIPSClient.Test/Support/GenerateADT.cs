using HIPSClient.Hips.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Test.Support
{
  public class GenerateADT
  {
    public ADT A01()
    {
      var ADT = new ADT();
      ADT.Patient = new Patient();
      ADT.Patient.Family = "ENGLISH";
      ADT.Patient.Given = "CLINTON";
      ADT.Patient.Title = "Mr";
      ADT.Patient.DateOfBirth = new DateTime(1957, 02, 14);
      ADT.Patient.Gender = Gender.Male;

      ADT.Patient.IndigenousStatus = new IndigenousStatus()
      {
        IndigenousStatusType = IndigenousStatusType.NotStatedInadequatelyDescribed
      };

      ADT.Patient.Address = new Address()
      {
        AddressLineOne = "Unit 1",
        AddressLineTwo = "111 Adha Street",
        Suburb = "Brisbane",
        PostCode = "4000",
        State = "QLD",
        Country = "AUS"
      };

      ADT.Patient.HomeContact = new Contact()
      {
        Value = "93235615"
      };

      ADT.Patient.WorkContact = new Contact()
      {
        Value = "0414778341"
      };

      ADT.Patient.IdentifierList = new List<PatientIdentifier>();

      //MRN
      ADT.Patient.IdentifierList.Add(new PatientIdentifier()
      {
        Value = "2142363",
        Type = PatientIdentifierType.MedicalRecordNumber,
        AssigningAuthority = Common.HIPS.HipsConfig.HospitalCode
      });

      //Medicare Number
      ADT.Patient.IdentifierList.Add(new PatientIdentifier()
      {
        Value = "6951129981",
        Type = PatientIdentifierType.MedicareNumber
      });


      ADT.HospitalEncounter = new HospitalEncounter()
      {
        PatientClass = PatientClassType.InPatient,
        VisitNumber = "000002",
        Bed = "Bed",
        Room = "Room",
        Ward = "Ward",
        AdmissionDate = new DateTime(2019, 01, 01),
        DischargeDate = null
      };

      return ADT;
    }
  }
}
