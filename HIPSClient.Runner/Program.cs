using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIPSClient.Hips.DatabaseLoader;
using HIPSClient.Hips.Model;
using PeterPiper.Hl7.V2.Model;

namespace HIPSClient.Runner
{
  class Program
  {
    static void Main(string[] args)
    {
      var ADT_A01 = new ADT_A01();
      ADT_A01.Patient = new Patient();
      ADT_A01.Patient.Family = "CURRY";
      ADT_A01.Patient.Given = "ELLENS";
      ADT_A01.Patient.Title = "Ms";
      ADT_A01.Patient.DateOfBirth = new DateTime(2003, 09, 15);
      ADT_A01.Patient.Gender = Gender.Female;

      ADT_A01.Patient.IndigenousStatus = new IndigenousStatus()
      {
        IndigenousStatusType = IndigenousStatusType.NotStatedInadequatelyDescribed
      };

      ADT_A01.Patient.Address = new Address()
      {
        AddressLineOne = "",
        AddressLineTwo = "9 HERB AVE",
        Suburb = "BROADBEACH",
        PostCode = "4218",
        State = "QLD",
        Country = "AUS"
      };

      ADT_A01.Patient.HomeContact = new Contact()
      {
        Value = "93235615"
      };

      ADT_A01.Patient.WorkContact = new Contact()
      {
        Value = "0414778341"
      };

      ADT_A01.Patient.IdentifierList = new List<Identifier>();

      //MRN
      ADT_A01.Patient.IdentifierList.Add(new Identifier()
      {
        Value = "0000020",
        Type = PatientIdentifierType.MedicalRecordNumber,
        AssigningAuthority = Common.HIPS.HipsConfig.HospitalCode
      });

      //Medicare Number
      ADT_A01.Patient.IdentifierList.Add(new Identifier()
      {
        Value = "4950827451",
        Type = PatientIdentifierType.MedicareNumber
      });


      ADT_A01.HospitalEncounter = new HospitalEncounter()
      {
        PatientClass = PatientClassType.InPatient,
        VisitNumber = "000001",
        Bed = "Bed",
        Room = "Room",
        Ward = "Ward",
        AdmissionDate = new DateTime(2019, 01, 01),
        DischargeDate = null
      };

      
      var DatabaseLoader = new DatabaseLoaderClient();
      var Response = DatabaseLoader.ADT(new DatabaseLoaderRequest() {  ADT_A01 = ADT_A01 });

    }
  }
}
