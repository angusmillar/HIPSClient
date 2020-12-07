using System;
using System.Collections.Generic;
using HIPSClient.Hips.ConsentService;
//using HIPSClient.Hips.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HIPSClient.Test
{
  [TestClass]
  public class Test_ConsentCheck
  {
    [TestMethod]
    public void Test_Consent()
    {
      //Prepare      
      var Client = new ConsentServiceClient();

      //Act
      var Response =  Client.CheckConsent(new CheckConsentRequest());
      
      //Assert
      //Assert.IsTrue(Response.IsSuccess);
    }
    
  }
}
