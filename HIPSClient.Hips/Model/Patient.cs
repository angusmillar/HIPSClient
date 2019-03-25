using HIPSClient.Common.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.Model
{
  public enum Gender
  {
    [EnumUIDisplay("Male")]
    [EnumLiteral("M")]
    Male,
    [EnumUIDisplay("Female")]
    [EnumLiteral("F")]
    Female,
    [EnumUIDisplay("Unknown")]
    [EnumLiteral("U")]
    Unknown,
    [EnumUIDisplay("Other")]
    [EnumLiteral("O")]
    Other
  }

  public class Patient
  {
    public string Family { get; set; }
    public string Given { get; set; }
    public string Title { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public IndigenousStatus IndigenousStatus { get; set; }
    public PatientIdentifier StateIdentifier { get; set; }
    public List<PatientIdentifier> IdentifierList { get; set; }
    public Address Address { get; set; }
    public Contact HomeContact { get; set; }
    public Contact WorkContact { get; set; }
    public DateTimeOffset? DateOfDeath { get; set; }

  }
}
