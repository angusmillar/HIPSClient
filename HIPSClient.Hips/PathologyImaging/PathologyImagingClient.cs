using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.PathologyImaging
{
  public class PathologyImagingClient
  {
    public PathologyImagingResponse Path(PathologyImagingRequest Request)
    {
      var Response = new PathologyImagingResponse();
      
      using (var client = new HipsPathologyImagingService.PathologyImagingServiceClient("WSHttpBinding_IPathologyImagingService"))
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
          var ClientResponse = client.UploadOrRemovePathology(Request.HL7ORUMessage, LocalUser, null, "", "", out Messages);
          
          return Response;

        }
        catch (System.Exception Exec)
        {
          Response.IsSuccess = false;
          Response.Message = Exec.Message;
          return Response;
        }
      }
    }
  }
}
