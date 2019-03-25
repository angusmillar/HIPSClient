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
    [EnumUIDisplay("Medicare Number")]
    [EnumLiteral("MC")]
    MedicareNumber, 
    /// <summary>
    /// Individual Healthcare Identifier
    /// </summary>
    [EnumUIDisplay("IHI")]
    [EnumLiteral("NI")]
    IHI,
    /// <summary>
    /// Department of Veterans Affairs Number
    /// </summary>
    [EnumUIDisplay("DVA")]
    [EnumLiteral("DVA")]
    DVA,
    /// <summary>
    /// Hospital Medicare Record Number
    /// </summary>
    [EnumUIDisplay("Medical Record Number")]
    [EnumLiteral("MR")]
    MedicalRecordNumber,
    /// <summary>
    /// EMPI state wide identifier
    /// </summary>
    [EnumUIDisplay("State Identifier")]
    [EnumLiteral("StatePatientId")]
    StatePatientId,
    /// <summary>
    /// PI: Used by pathology and DI when the identifier is their internal patient key
    /// </summary>
    [EnumUIDisplay("Patient Identifier")]
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
        string PredefinedAssigingAuthority = GetAssigningAuthorityForPatientIdentifierType(Type);
        if (PredefinedAssigingAuthority == string.Empty)
          return _AssigningAuthority;
        else
          return PredefinedAssigingAuthority;        
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

    public static string GetAssigningAuthorityForPatientIdentifierType(PatientIdentifierType Type)
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
          return string.Empty;
        case PatientIdentifierType.PatientInternalIdentifier:
          return string.Empty;
        case PatientIdentifierType.StatePatientId:
          return PatientIdentifierType.StatePatientId.GetLiteral();
        default:
          throw new System.ComponentModel.InvalidEnumArgumentException(Type.ToString(), (int)Type, typeof(PatientIdentifierType));
      }
    }
  }
}
