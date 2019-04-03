using System;
using System.Collections.Generic;
using HIPSClient.Hips.PCEHRService;
using HIPSClient.Hips.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HIPSClient.Test
{
  [TestClass]
  public class Test_IsPCEHRAdvertised
  {
    [TestMethod]
    public void Test_IsPCEHRAdvertised_One()
    {
      //Prepare      
      var Client = new PCEHRServiceClient();

      //Act
      var Response =  Client.IsPcehrAdvertised(new IsPcehrAdvertisedRequest());
      
      //Assert
      //Assert.IsTrue(Response.IsSuccess);
    }
    
  }
}
