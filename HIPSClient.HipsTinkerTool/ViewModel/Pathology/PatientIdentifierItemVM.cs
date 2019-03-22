using HIPSClient.Common.Tools.Enum;
using HIPSClient.Common.Tools.String;
using Identifiers.Australian.DepartmentVeteransAffairs;
using Identifiers.Australian.MedicareNumber;
using Identifiers.Australian.NationalHealthcareIdentifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.HipsTinkerTool.ViewModel.Pathology
{
  public class PatientIdentifierItemVM : BaseValidationVM
  {
    private IDictionary<string, int> PatientIdentifierTypeDictionary;
    public PatientIdentifierItemVM()
    {
      PatientIdentifierTypeDictionary = HIPSClient.Common.Tools.Enum.EnumUtility.ConvertEnumToDictionary<HIPSClient.Hips.Model.PatientIdentifierType>();      
    }

    public PatientIdentifierItemVM(HIPSClient.Hips.Model.PatientIdentifier PatientIdentifier) 
      : this()
    {
      _Value = PatientIdentifier.Value;
      _IdentifierType = PatientIdentifier.IdentifierTypeCode;
      _AssigningAuthority = PatientIdentifier.AssigningAuthority;
      _Type = PatientIdentifier.Type;      
    }
    
    private string _Value;
    public string Value
    {
      get
      {
        return _Value;
      }
      set
      {
        _Value = value;
        OnPropertyChanged("Value");
      }
    }

    private string _IdentifierType;
    public string IdentifierType
    {
      get
      {
        return _IdentifierType;
      }
      set
      {
        _IdentifierType = value;
        OnPropertyChanged("IdentifierType");
      }
    }

    private string _AssigningAuthority;
    public string AssigningAuthority
    {
      get
      {
        return _AssigningAuthority;
      }
      set
      {
        _AssigningAuthority = value;
        OnPropertyChanged("AssigningAuthority");
      }
    }

    private HIPSClient.Hips.Model.PatientIdentifierType _Type;
    public string Type
    {
      get
      {
        return _Type.ToString();
      }
      set
      {               
        _Type = (HIPSClient.Hips.Model.PatientIdentifierType)PatientIdentifierTypeDictionary[value];
        this.IdentifierType = _Type.GetLiteral();     
        if (_Type != Hips.Model.PatientIdentifierType.MedicalRecordNumber && _Type != Hips.Model.PatientIdentifierType.PatientInternalIdentifier)
        {
          this.AssigningAuthority = HIPSClient.Hips.Model.PatientIdentifier.GetAssigningAuthorityForPatientIdentifierType(_Type);
        }
        else
        {
          this.AssigningAuthority = _AssigningAuthority;
        }
        this.Value = string.Empty;

        ClearAllErrors();
        OnPropertyChanged("Type");
        OnPropertyChanged("Value");
        OnPropertyChanged("AssigningAuthority");
       
      }
    }

    protected override string IsValid(string PropertyName)
    {    
      //No need to validate IdentiferType as it is readonly and always set by code.

      string AssAuthCode = HIPSClient.Hips.Model.PatientIdentifier.GetAssigningAuthorityForPatientIdentifierType(_Type).ToUpper();
      if (PropertyName == "AssigningAuthority")
      {
        if (_Type != Hips.Model.PatientIdentifierType.MedicalRecordNumber && _Type != Hips.Model.PatientIdentifierType.PatientInternalIdentifier)
        {
          if (this.AssigningAuthority != null && this.AssigningAuthority.ToUpper() != AssAuthCode || this.AssigningAuthority == null)
          {
            AddError("AssigningAuthority", $"The Assigning Authority for a {_Type.ToString()} must be {AssAuthCode}");
            return "Error Found!";
          }            
        }
        if (string.IsNullOrWhiteSpace(this.AssigningAuthority))
        {
          AddError("AssigningAuthority", $"The Assigning Authority must be populated");
          return "Error Found!";
        }
        RemoveError("AssigningAuthority");
      }

      if (PropertyName == "Value")
      {
        switch (_Type)
        {
          case Hips.Model.PatientIdentifierType.MedicareNumber:
            {
              IMedicareNumberParser Parser = new MedicareNumberParser();
              IMedicareNumber MedNum;
              if (string.IsNullOrWhiteSpace(this.Value) || this.Value == null || !Parser.TryParse(this.Value.RemoveWhitespace(), out MedNum))
              {
                AddError("Value", "The Medicare Number does not pass validation.");
                return "Error Found!";
              }
            }
            break;
          case Hips.Model.PatientIdentifierType.IHI:
            {
              IIndividualHealthcareIdentifier IHI;
              IIndividualHealthcareIdentifierParser Parser = new IndividualHealthcareIdentifierParser();
              if (string.IsNullOrWhiteSpace(this.Value) || this.Value == null || !Parser.TryParse(this.Value.RemoveWhitespace(), out IHI))
              {
                AddError("Value", "The IHI number does not pass validation.");
                return "Error Found!";
              }
            }
            break;
          case Hips.Model.PatientIdentifierType.DVA:
            {
              IDVANumberParser Parser = new DVANumberParser();
              IDVANumber DVA;
              if (this.Value == null || !Parser.TryParse(this.Value.RemoveWhitespace(), out DVA))
              {
                AddError("Value", "The DVA number does not pass validation.");
                return "Error Found!";
              }
            }
            break;
          case Hips.Model.PatientIdentifierType.MedicalRecordNumber:
            if (string.IsNullOrWhiteSpace(this.Value))
            {
              AddError("Value", "The Medical Record Number must be populated.");
              return "Error Found!";
            }
            break;
          case Hips.Model.PatientIdentifierType.StatePatientId:
            if (string.IsNullOrWhiteSpace(this.Value))
            {
              AddError("Value", "The State Patient Id value must be populated.");
              return "Error Found!";
            }
            break;
          case Hips.Model.PatientIdentifierType.PatientInternalIdentifier:
            if (string.IsNullOrWhiteSpace(this.Value))
            {
              AddError("Value", "The Patient Internal Identifier value must be populated.");
              return "Error Found!";
            }
            break;
          default:
            break;
        }
        RemoveError("Value");
      }

      return string.Empty;
    }
  }
}
