using HIPSClient.Hips.HipsPCEHRService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.PCEHRService
{
  public class PCEHRServiceClient
  {
    public IsPcehrAdvertisedResponse IsPcehrAdvertised(IsPcehrAdvertisedRequest Request)
    {
      var Response = new IsPcehrAdvertisedResponse();      
      string MethodAddress = "PCEHRService/HIPS.Service.PCEHRService";     
      WSHttpBinding Binding = new WSHttpBinding(SecurityMode.None);
      Uri EndpointUri = new Uri(Common.HIPS.HipsConfig.CoreApplicationBaseEndpoint, MethodAddress);
      EndpointAddress EndpointAddress = new EndpointAddress(EndpointUri);
      using (var client = new HipsPCEHRService.PCEHRServiceClient(Binding, EndpointAddress))      
      {        
        try
        {
          
          ValidatedIhi id = new ValidatedIhi();
          
          id.DateOfBirth = new DateTime(1984, 04, 04);
          id.FamilyName = "Goldmen";
          id.GivenName = "Wallace";
          id.HospitalCode = "ANGPATH";
          id.HospitalCodeSystem = "pasFacCd";
          id.Sex = HipsPCEHRService.SexEnumerator.Male;
          id.Ihi = "8003608000094961";
          id.IhiRecordStatus = IhiRecordStatus.Verified;
          id.IhiStatus = IhiStatus.Active;
          id.IhiLastValidated = new DateTime(2019, 01, 06, 11, 00, 00);

          UserDetails User = new UserDetails();
          User.Role = UserRole.AuthorisedEmployee;

          HospitalIdentifier HospitalId = new HospitalIdentifier();
          HospitalId.HospitalCode = id.HospitalCode;
          HospitalId.HospitalCodeSystem = id.HospitalCodeSystem;


          //HipsPathologyImagingService.Message[] Messages;
          //var ClientResponse = client.UploadOrSupersedeDocument()
          //var ClientResponse = client.IsPcehrAdvertised(id, new DateTime(1957, 02, 14), User);
          
          var ClientResponse = client.RefreshPatientParticipationStatus(User, id, HospitalId, ForceRefreshOption.WhenNotAdvertised);
          //throw new NotImplementedException();
          return Response;
          
        }
        catch (System.Exception Exec)
        {          
          Response.Message = Exec.Message;
          return Response;
        }
      }
    }
  }
}
