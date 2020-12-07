using HIPSClient.Hips.HipsConsentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.ConsentService
{
  public class ConsentServiceClient
  {

    public CheckConsentResponse CheckConsent(CheckConsentRequest Request)
    {
      var Response = new CheckConsentResponse();
      string MethodAddress = "ConsentService/HIPS.Service.ConsentService";
      WSHttpBinding Binding = new WSHttpBinding(SecurityMode.None);
      Uri EndpointUri = new Uri(Common.HIPS.HipsConfig.CoreApplicationBaseEndpoint, MethodAddress);
      EndpointAddress EndpointAddress = new EndpointAddress(EndpointUri);
      using (var client = new HipsConsentService.ConsentServiceClient(Binding, EndpointAddress))
      {
        var LocalUser = new HipsConsentService.UserDetails()
        {
          Domain = "ADHA",
          Name = "Angus Millar",
          AuthorisedEmployeeUserId = "ANGUSM",
          Login = "AngusM",
          Role = UserRole.AuthorisedEmployee
        };

        Mrn MRN = new Mrn();
        MRN.HospitalCode = "ANGPATH";
        MRN.HospitalCodeSystem = "pasFacCd";
        MRN.Value = "000000101";


        //ValidatedIhi id = new ValidatedIhi();

        ////English
        //id.DateOfBirth = new DateTime(1957, 02, 14);
        //id.FamilyName = "ENGLISH";
        //id.GivenName = "CLINTON";
        //id.HospitalCode = "ANGPATH";
        //id.HospitalCodeSystem = "pasFacCd";
        //id.Sex = SexEnumerator.Male;
        //id.Ihi = "8003608333512671";
        //id.IhiRecordStatus = IhiRecordStatus.Verified;
        //id.IhiStatus = IhiStatus.Active;
        //id.IhiLastValidated = new DateTime(2000, 01, 01);

        //
        //id.DateOfBirth = new DateTime(1957, 02, 14);
        //id.FamilyName = "GOLDMEN";
        //id.GivenName = "Wallace";
        //id.HospitalCode = "ANGPATH";
        //id.HospitalCodeSystem = "pasFacCd";
        //id.Sex = SexEnumerator.Male;
        //id.Ihi = "8003608000094961";
        //id.IhiRecordStatus = IhiRecordStatus.Unknown;
        //id.IhiStatus = IhiStatus.Unknown;
        //id.IhiLastValidated = new DateTime(2015, 01, 01, 08, 00, 00);

        try
        {
          var ClientResponse = client.CheckConsent(MRN, DateTime.Now.Subtract(new TimeSpan(48,0,0)), LocalUser);
          Response.ConstentStatus = ClientResponse.ConsentStatus.ToString();
          return Response;
        }
        catch (Exception Exec)
        {
          Response.Message = Exec.Message;
          return Response;
        }

      }
    }
  }
}
