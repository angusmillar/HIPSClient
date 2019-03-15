using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.PathologyImaging
{
  public class PathologyImagingClient
  {
    public PathologyImagingResponse UploadPathologyReport(PathologyImagingRequest Request)
    {
      var Response = new PathologyImagingResponse();                             
      string MethodAddress = "PathologyImagingService/HIPS.Service.PathologyImagingService";      
      WSHttpBinding Binding = new WSHttpBinding(SecurityMode.None);
      Uri EndpointUri = new Uri(Common.HIPS.HipsConfig.CoreApplicationBaseEndpoint, MethodAddress);
      EndpointAddress EndpointAddress = new EndpointAddress(EndpointUri);
      using (var client = new HipsPathologyImagingService.PathologyImagingServiceClient(Binding, EndpointAddress))
      {
        var LocalUser = new HipsPathologyImagingService.LocalUser()
        {
          Domain = "ADHA",
          FamilyName = "Millar",
          GivenNames = "Angus",
          Login = "AngusM"
        };

        try
        {
          HipsPathologyImagingService.Message[] Messages;
          var ClientResponse = client.UploadOrRemovePathology(Request.GetHL7Message, LocalUser, null, "", "", out Messages);
          switch (ClientResponse)
          {
            case HipsPathologyImagingService.ResponseStatus.None:
              Response.Status = PathologyImagingResponseStatus.None;              
              break;
            case HipsPathologyImagingService.ResponseStatus.OK:
              Response.Status = PathologyImagingResponseStatus.OK; 
              break;
            case HipsPathologyImagingService.ResponseStatus.Warning:
              Response.Status = PathologyImagingResponseStatus.Warning;
              break;
            default:
              throw new System.ComponentModel.InvalidEnumArgumentException(ClientResponse.ToString(), (int)ClientResponse, typeof(HipsPathologyImagingService.ResponseStatus));              
          }
          if (Messages != null && Messages.Count() > 0)
          {
            foreach(var item in Messages)
            {
              Response.Message = Response.Message + " , " + item.Description;
            }
          }

          return Response;

        }
        catch (System.Exception Exec)
        {
          Response.Status = PathologyImagingResponseStatus.Error;
          Response.Message = Exec.Message;
          return Response;
        }
      }
    }
  }
}
