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
    public ADT A01_English()
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

    public ADT A01_Hungerford()
    {
      var ADT = new ADT();
      ADT.Patient = new Patient();
      ADT.Patient.Family = "Hungerford";
      ADT.Patient.Given = "Isabella";
      ADT.Patient.Title = "Ms";
      ADT.Patient.DateOfBirth = new DateTime(1980, 10, 05);
      ADT.Patient.Gender = Gender.Female;

      ADT.Patient.IndigenousStatus = new IndigenousStatus()
      {
        IndigenousStatusType = IndigenousStatusType.NotStatedInadequatelyDescribed
      };

      ADT.Patient.Address = new Address()
      {
        AddressLineOne = "1 Struggle St",
        AddressLineTwo = "",
        Suburb = "DALWALLINU",
        PostCode = "6609",
        State = "WA",
        Country = "AUS"
      };

      //ADT.Patient.HomeContact = new Contact()
      //{
      //  Value = "93235615"
      //};

      //ADT.Patient.WorkContact = new Contact()
      //{
      //  Value = "0414778341"
      //};

      ADT.Patient.IdentifierList = new List<PatientIdentifier>();

      //MRN
      ADT.Patient.IdentifierList.Add(new PatientIdentifier()
      {
        Value = "000000101",
        Type = PatientIdentifierType.MedicalRecordNumber,
        AssigningAuthority = Common.HIPS.HipsConfig.HospitalCode
      });

      //Medicare Number
      ADT.Patient.IdentifierList.Add(new PatientIdentifier()
      {
        Value = "69504328711",
        Type = PatientIdentifierType.MedicareNumber
      });


      ADT.HospitalEncounter = new HospitalEncounter()
      {
        PatientClass = PatientClassType.InPatient,
        VisitNumber = "000004",
        Bed = "Bed",
        Room = "Room",
        Ward = "Ward",
        AdmissionDate = new DateTime(2019, 03, 18),
        DischargeDate = null
      };

      return ADT;
    }

    public ADT A01_SallyBiscuit()
    {
      var ADT = new ADT();
      ADT.Patient = new Patient();
      ADT.Patient.Family = "Biscuit";
      ADT.Patient.Given = "Sally";
      ADT.Patient.Title = "Ms";
      ADT.Patient.DateOfBirth = new DateTime(1984, 04, 04);
      ADT.Patient.Gender = Gender.Female;

      ADT.Patient.IndigenousStatus = new IndigenousStatus()
      {
        IndigenousStatusType = IndigenousStatusType.NotStatedInadequatelyDescribed
      };

      ADT.Patient.Address = new Address()
      {
        AddressLineOne = "5 Cook Street",
        AddressLineTwo = "",
        Suburb = "Red Hill",
        PostCode = "4059",
        State = "QLD",
        Country = "AUS"
      };

      //ADT.Patient.HomeContact = new Contact()
      //{
      //  Value = "93235615"
      //};

      //ADT.Patient.WorkContact = new Contact()
      //{
      //  Value = "0414778341"
      //};

      ADT.Patient.IdentifierList = new List<PatientIdentifier>();

      //MRN
      ADT.Patient.IdentifierList.Add(new PatientIdentifier()
      {
        Value = "000000102",
        Type = PatientIdentifierType.MedicalRecordNumber,
        AssigningAuthority = Common.HIPS.HipsConfig.HospitalCode
      });

      //Medicare Number
      ADT.Patient.IdentifierList.Add(new PatientIdentifier()
      {
        Value = "49504816511",
        Type = PatientIdentifierType.MedicareNumber
      });


      ADT.HospitalEncounter = new HospitalEncounter()
      {
        PatientClass = PatientClassType.InPatient,
        VisitNumber = "000005",
        Bed = "Bed",
        Room = "Room",
        Ward = "Ward",
        AdmissionDate = new DateTime(2019, 03, 18),
        DischargeDate = null
      };

      return ADT;
    }
  }
}
