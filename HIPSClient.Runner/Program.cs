using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIPSClient.Hips.DatabaseLoader;
using PeterPiper.Hl7.V2.Model;

namespace HIPSClient.Runner
{
  class Program
  {
    static void Main(string[] args)
    {
      var DatabaseLoader = new DatabaseLoaderClient();

      var Msg = new List<string>();
      Msg.Add(@"MSH|^~\&|ADT|ANGPATH|HIB|HIB|20181205100000||ADT^A01|2012112307233417184242|P|2.3.1|||AL|NE|AU|ASCII|EN");
      Msg.Add(@"EVN|A08|20121123072334");
      Msg.Add(@"PID|||0000020^^^ANGPATH^MR~4950827451^^^AUSHIC^MC~8003608333513877^^^AUSHIC^NI||CURRY^ELLENS^^^Ms^^L||20030915|F||1^Aboriginal but not Torres Strait Islander origin^ISAAC|^9 HERB AVE^BROADBEACH^^4218^AUS^H|||^PRN^PH^^^^93235615|^WPN^CP^^^^0414778341");
      Msg.Add(@"PV1||I|Ward1^Room1^Bed1||||||||||||||||00000001|||||||||||||||||||||||||20180926|");
      var HL7 = Creator.Message(Msg);


      var Response = DatabaseLoader.ADT(new DatabaseLoaderRequest() { HL7ADTMessage = HL7.AsStringRaw });

    }
  }
}
