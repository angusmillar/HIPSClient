using System;
using System.Collections.Generic;
using System.Linq;
using HIPSClient.Hips.DatabaseLoader;
using HIPSClient.Hips.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HIPSClient.Test
{
  [TestClass]
  public class Test_PathologyORU
  {
    [TestMethod]
    public void TestPathologyORU()
    {
      //We need to send an ADT first to ensure the patient is registered first
      //This is only needed the first time it is run against a new HIPS instance
      //But we do it every time to protect against it ever failing
      
      //Prepare 
      var GenerateADT = new Support.GenerateADT();
      var DatabaseLoader = new DatabaseLoaderClient();
      var ADT = GenerateADT.A01();

      //Act
      var Response = DatabaseLoader.ADT(new DatabaseLoaderRequest()
      {
        EventType = HL7EventType.A01,
        ADT_A01 = GenerateADT.A01()
      });

      //Assert
      Assert.IsTrue(Response.IsSuccess);

      //Now for the Pathology Report
      //##################################################################

      //Prepare 
      var ORU = new ORU();
      ORU.Patient = ADT.Patient;
      //Switch the patient MRN from Hospital Authority Code to the LIS Authority code.
      var PrimaryIdentifier = ORU.Patient.IdentifierList.SingleOrDefault(x => x.AssigningAuthority == Common.HIPS.HipsConfig.HospitalCode);
      PrimaryIdentifier.AssigningAuthority = Common.HIPS.HipsConfig.LISHospitalCode;
      PrimaryIdentifier.Type = PatientIdentifierType.PatientInternalIdentifier;

      ORU.HospitalEncounter = ADT.HospitalEncounter;

      ORU.Order = new PathologyOrder();
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
      Request.OrderIdentifier = "0000002";
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

      //Act
      Hips.PathologyImaging.PathologyImagingResponse PathologyResponse = PathClient.UploadPathologyReport(new Hips.PathologyImaging.PathologyImagingRequest() { ORU = ORU });

      //Assert
      Assert.AreEqual(Hips.PathologyImaging.PathologyImagingResponseStatus.OK, PathologyResponse.Status);

    }
  }
}
