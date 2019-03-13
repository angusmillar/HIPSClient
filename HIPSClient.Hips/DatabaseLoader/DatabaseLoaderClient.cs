using System;
using System.ServiceModel;
using HIPSClient.Common.Tools.String;
using PeterPiper.Hl7.V2.Model;

namespace HIPSClient.Hips.DatabaseLoader
{
  public class DatabaseLoaderClient
  {
    public DatabaseLoaderResponse ADT(DatabaseLoaderRequest Request)
    {
      if (Request.ADT_A01 == null && Request.HL7ADTMessage.IsSet())
        throw new HIPSClient.Common.CustomException.HipsClientException("ADT_A01 and HL7ADTMessage have been both set, only one or the other can be used at once.");

      string MethodAddress = "DatabaseLoaderService/HIPS.Service.DatabaseLoaderService";
      var Response = new DatabaseLoaderResponse();
      WSHttpBinding Binding = new WSHttpBinding(SecurityMode.None);
      Uri EndpointUri = new Uri(Common.HIPS.HipsConfig.CoreApplicationBaseEndpoint, MethodAddress);
      EndpointAddress EndpointAddress = new EndpointAddress(EndpointUri);
      using (var client = new HipsDatabaseLoaderService.DatabaseLoaderServiceClient(Binding, EndpointAddress))      
      {
        var UserDetails = new HipsDatabaseLoaderService.UserDetails()
        {
          Role = HipsDatabaseLoaderService.UserRole.AuthorisedEmployee
        };
        try
        {
          var ClientResponse = client.NotifyPasEvent(Request.GetHL7Message, UserDetails);
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
