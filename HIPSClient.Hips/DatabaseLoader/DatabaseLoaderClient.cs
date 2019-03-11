using System;
using System.ServiceModel;
using PeterPiper.Hl7.V2.Model;

namespace HIPSClient.Hips.DatabaseLoader
{
  public class DatabaseLoaderClient
  {
    public DatabaseLoaderResponse ADT(DatabaseLoaderRequest Request)
    {
      string MethodAddress = "DatabaseLoaderService/HIPS.Service.DatabaseLoaderService";
      var Response = new DatabaseLoaderResponse();
      WSHttpBinding Binding = new WSHttpBinding(SecurityMode.None);
      EndpointAddress EndpointAddress = new EndpointAddress(System.IO.Path.Combine(Common.HIPS.HipsConfig.CoreApplicationBaseEndpoint, MethodAddress));
      using (var client = new HipsDatabaseLoaderService.DatabaseLoaderServiceClient(Binding, EndpointAddress))      
      {
        var UserDetails = new HipsDatabaseLoaderService.UserDetails()
        {
          Role = HipsDatabaseLoaderService.UserRole.AuthorisedEmployee
        };
        try
        {
          var ClientResponse = client.NotifyPasEvent(Request.HL7ADTMessage, UserDetails);
          var HL7Ack = Creator.Message(ClientResponse);
          if (HL7Ack.Segment("MSA").Field(1).AsString == "AA")
          {
            Response.IsSuccess = true;
          }
          else
          {
            Response.IsSuccess = false;
            Response.Message = HL7Ack.Segment("MSA").Field(6).Component(2).AsString;
          }

          return Response;

        }
        catch (Exception Exec)
        {
          Response.IsSuccess = false;
          Response.Message = Exec.Message;
          return Response;
        }
      }      
    }
  }
}
