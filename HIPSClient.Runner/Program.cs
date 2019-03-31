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

      
      var DatabaseLoader = new DatabaseLoaderClient();
      var Response = DatabaseLoader.ADT(new DatabaseLoaderRequest()
      {
        EventType = HL7EventType.A01,
        ADT_A01 = ADT
      });

      //======== Pathology =========================================

      var ORU = new ORU();
      ORU.Patient = ADT.Patient;
      //Switch the patient MRN from Hospital Authority Code to the LIS Authority code.
      var PrimaryIdentifier = ORU.Patient.IdentifierList.SingleOrDefault(x => x.AssigningAuthority == Common.HIPS.HipsConfig.HospitalCode);
      PrimaryIdentifier.AssigningAuthority = Common.HIPS.HipsConfig.LISHospitalCode;
      PrimaryIdentifier.Type = PatientIdentifierType.PatientInternalIdentifier;

      ORU.HospitalEncounter = ADT.HospitalEncounter;

      ORU.Order = new PathologyOrder();
      ORU.Order.OrderIdentifier = "0000002";
      ORU.Order.OrderedDateTime = new DateTimeOffset(2019, 03, 15, 08, 45, 00, new TimeSpan(10, 0, 0));
      ORU.Order.CollectionDateTime = new DateTimeOffset(2019, 03, 15, 10, 30, 00, new TimeSpan(10, 0, 0));

      ORU.Order.OrderingProvider = new Provider();
      ORU.Order.OrderingProvider.Family = "Millar";
      ORU.Order.OrderingProvider.Given = "Angus";
      ORU.Order.OrderingProvider.Title = "Mr";
      ORU.Order.OrderingProvider.Identifer = new ProviderIdentifier()
      {
        Type = ProviderIdentifierType.Local,
        AssigningAuthority = "PathWest",
        Value = "123456"
      };

      //ORU.Order.IsMyHealthRecordDisclosed = false;

      ORU.RequestList = new List<PathologyRequest>();
      var Request = new PathologyRequest();
      ORU.RequestList.Add(Request);
      
      Request.ReportIdentifier = "ADHA.P19-00000002-FBP-0";
      Request.ReportedDateTime = new DateTimeOffset(2019, 03, 15, 14, 15, 00, 00, new TimeSpan(10, 0, 0));
      Request.DepartmentCode = "HM";
      Request.ReportName = new UniversalServiceIdentifier()
      {
        LocalCode = "HFBP",
        LocalCodeDescription = "FULL BLOOD PICTURE",
        LocalCodeSystemCode = "PATHWEST",
        SnomedTermValue = "26604007",
        SnomedPreferedTerm = "Full blood count"
      };

      Request.DocumentAuthor = new Provider()
      {
        Family = "Holmes",
        Given = "Harry",
        Title = "Dr",
        Identifer = new ProviderIdentifier()
        {
          Type = ProviderIdentifierType.HPII,
          Value = "8003613233362573"
        }
      };
      
      Request.ReportStatus = ResultStatus.Final;

      ORU.PDF = new PDFReport()
      {
        Filepath = @"C:\GitRepository\HL7V2Examples\Pathology\NEHTA AS4700.2 2012 Examples\Result Output Example 1\FBC NEHTA Pathology Report PDF.pdf",
        ResultStatus = ResultStatus.Final        
      };

      HIPSClient.Hips.PathologyImaging.PathologyImagingClient PathClient = new Hips.PathologyImaging.PathologyImagingClient();
      Hips.PathologyImaging.PathologyImagingResponse PathologyResponse = PathClient.UploadPathologyReport(new Hips.PathologyImaging.PathologyImagingRequest() { ORU = ORU });

    }
  }
}
