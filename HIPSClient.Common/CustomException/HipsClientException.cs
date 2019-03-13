using System;

namespace HIPSClient.Common.CustomException
{
  public class HipsClientException : ApplicationException
  {
    public HipsClientException(string message) 
      : base(message)
    {

    }

    public HipsClientException(string message, Exception exception)
      : base(message, exception)
    {

    }
  }
}
