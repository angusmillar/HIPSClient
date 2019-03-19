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
    public void TestADT_A01_English()
    {
      //Prepare
      var GenerateADT = new Support.GenerateADT();      
      var DatabaseLoader = new DatabaseLoaderClient();

      //Act
      var Response = DatabaseLoader.ADT(new DatabaseLoaderRequest()
      {
        EventType = HL7EventType.A01,
        ADT_A01 = GenerateADT.A01_English()
      });

      //Assert
      Assert.IsTrue(Response.IsSuccess);
    }

    [TestMethod]
    public void TestADT_A01_Hungerford()
    {
      //Prepare
      var GenerateADT = new Support.GenerateADT();
      var DatabaseLoader = new DatabaseLoaderClient();

      //Act
      var Response = DatabaseLoader.ADT(new DatabaseLoaderRequest()
      {
        EventType = HL7EventType.A01,
        ADT_A01 = GenerateADT.A01_Hungerford()
      });

      //Assert
      Assert.IsTrue(Response.IsSuccess);
    }
  }
}
