using HIPSClient.Common.Tools.Attributes;
using HIPSClient.Common.Tools.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.Model
{
  public enum PatientIdentifierType
  {
    /// <summary>
    /// Australian Medicare Number
    /// </summary>
    [EnumLiteral("MC")]
    MedicareNumber,
    /// <summary>
    /// Individual Healthcare Identifier
    /// </summary>
    [EnumLiteral("NI")]
    IHI,
    /// <summary>
    /// Department of Veterans Affairs Number
    /// </summary>
    [EnumLiteral("DVA")]
    DVA,
    /// <summary>
    /// Hospital Medicare Record Number
    /// </summary>
    [EnumLiteral("MR")]
    MedicalRecordNumber,
    /// <summary>
    /// EMPI state wide identifier
    /// </summary>
    [EnumLiteral("StatePatientId")]
    StatePatientId,
    /// <summary>
    /// PI: Used by pathology and DI when the identifier is their internal patient key
    /// </summary>
    [EnumLiteral("PI")]   
    PatientInternalIdentifier
}

  public class PatientIdentifier
  {    
    public string Value { get; set; }

    private string _AssigningAuthority;
    public string AssigningAuthority
    {
      get
      {
        switch (Type)
        {
          case PatientIdentifierType.MedicareNumber:
            return Common.HIPS.HipsConfig.MedicareAssigningAuthority;
          case PatientIdentifierType.IHI:
            return Common.HIPS.HipsConfig.IHIAssigningAuthority;            
          case PatientIdentifierType.DVA:
            return Common.HIPS.HipsConfig.DVAAssigningAuthority;
          case PatientIdentifierType.MedicalRecordNumber:
            if (string.IsNullOrWhiteSpace(_AssigningAuthority))
              throw new ApplicationException($"You must set the AssigningAuthority for identifiers of PatientIdentifierType equal to {PatientIdentifierType.MedicalRecordNumber.ToString()} " +
                $"The identifier value of {this.Value} did not have an AssigningAuthority set. ");
            return _AssigningAuthority;
          case PatientIdentifierType.PatientInternalIdentifier:
            if (string.IsNullOrWhiteSpace(_AssigningAuthority))
              throw new ApplicationException($"You must set the AssigningAuthority for identifiers of PatientIdentifierType equal to {PatientIdentifierType.PatientInternalIdentifier.ToString()} " +
                $"The identifier value of {this.Value} did not have an AssigningAuthority set. ");
            return _AssigningAuthority;
          case PatientIdentifierType.StatePatientId:
            return PatientIdentifierType.StatePatientId.GetLiteral();
          default:
            throw new System.ComponentModel.InvalidEnumArgumentException(Type.ToString(), (int)Type, typeof(PatientIdentifierType));
        }        
      }
      set
      {
        _AssigningAuthority = value;
      }
    }
    
    public string IdentifierTypeCode
    {
      get
      {
        switch (Type)
        {
          case PatientIdentifierType.MedicareNumber:
            return PatientIdentifierType.MedicareNumber.GetLiteral();
          case PatientIdentifierType.IHI:
            return PatientIdentifierType.IHI.GetLiteral();
          case PatientIdentifierType.DVA:
            return PatientIdentifierType.DVA.GetLiteral();
          case PatientIdentifierType.MedicalRecordNumber:
            return PatientIdentifierType.MedicalRecordNumber.GetLiteral();
          case PatientIdentifierType.StatePatientId:
            return PatientIdentifierType.StatePatientId.GetLiteral();
          case PatientIdentifierType.PatientInternalIdentifier:
            return PatientIdentifierType.PatientInternalIdentifier.GetLiteral();
          default:
            throw new System.ComponentModel.InvalidEnumArgumentException(Type.ToString(), (int)Type, typeof(PatientIdentifierType));
        }
      }      
    }

    public PatientIdentifierType Type { get; set; }

  }
}
