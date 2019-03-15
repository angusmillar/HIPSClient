using System;
using System.Collections.Generic;
using HIPSClient.Hips.DatabaseLoader;
using HIPSClient.Hips.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HIPSClient.Test
{
  [TestClass]
  public class Test_ADT
  {
    [TestMethod]
    public void TestADT_A01()
    {
      //Prepare
      var GenerateADT = new Support.GenerateADT();      
      var DatabaseLoader = new DatabaseLoaderClient();

      //Act
      var Response = DatabaseLoader.ADT(new DatabaseLoaderRequest()
      {
        EventType = HL7EventType.A01,
        ADT_A01 = GenerateADT.A01()
      });

      //Assert
      Assert.IsTrue(Response.IsSuccess);
    }
  }
}
