using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIPSClient.Hips.DatabaseLoader;
using PeterPiper.Hl7.V2.Model;
using HIPSClient.Common.HL7Message;

namespace HIPSClient.Runner
{
  class Program
  {
    static void Main(string[] args)
    {
      var DatabaseLoader = new DatabaseLoaderClient();
      var ADT = new ADT();      

      var Response = DatabaseLoader.ADT(new DatabaseLoaderRequest() { HL7ADTMessage = ADT.A01() });

    }
  }
}
