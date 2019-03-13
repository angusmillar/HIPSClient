using HIPSClient.Common.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.Model
{
  public enum PatientClassType
  {
    [EnumLiteral("I")]
    InPatient,
    [EnumLiteral("O")]
    OutPatient,
    [EnumLiteral("E")]
    Emergency
  }

  public class HospitalEncounter
  {
    public PatientClassType PatientClass { get; set; }
    public string VisitNumber { get; set; }
    public string Bed { get; set; }
    public string Room { get; set; }
    public string Ward { get; set; }
    public DateTime? AdmissionDate { get; set; }
    public DateTime? DischargeDate { get; set; }
  }
}
