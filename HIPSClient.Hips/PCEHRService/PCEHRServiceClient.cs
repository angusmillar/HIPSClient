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
          
          id.DateOfBirth = new DateTime(1957, 02, 14);
          id.FamilyName = "ENGLISH";
          id.GivenName = "CLINTON";
          id.HospitalCode = "ANGPATH";
          id.HospitalCodeSystem = "pasFacCd";
          id.Sex = HipsPCEHRService.SexEnumerator.Male;
          id.Ihi = "8003608333512671";
          id.IhiRecordStatus = IhiRecordStatus.Unknown;
          id.IhiStatus = IhiStatus.Unknown;
          id.IhiLastValidated = new DateTime(2019, 04, 01, 08, 00, 00);

          UserDetails User = new UserDetails();
          User.Role = UserRole.AuthorisedEmployee;

          //HipsPathologyImagingService.Message[] Messages;
          //var ClientResponse = client.UploadOrSupersedeDocument()
          var ClientResponse = client.IsPcehrAdvertised(id, new DateTime(1957, 02, 14), User);
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
