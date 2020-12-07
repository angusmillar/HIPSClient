using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Common.HIPS
{
  public static class HipsConfig
  {    
    public static Uri CoreApplicationBaseEndpoint = new Uri("http://localhost:52500");
    

    public static string HospitalCode = "ANGPATH";
    public static string LISHospitalCode = "ANGUSLIS";

    public static string MedicareAssigningAuthority = "AUSHIC";
    public static string MedicareIdentifierTypeCode = "MC";

    public static string DVAAssigningAuthority = "AUSHIC";
    public static string DVAIdentifierTypeCode = "DVA";

    public static string IHIAssigningAuthority = "AUSHIC";
    public static string IHIIdentifierTypeCode = "NI";
    
    public static string MRNIdentifierTypeCode = "MR";


    public static string HPIIAssigningAuthority = "AUSHIC";
    public static string MedicareProviderNumberAssigningAuthority = "AUSHICPR";

    public static string MyHealthRecordDiscoveredFlagCode = "AUSEHR";
    
  }
}
