using HIPSClient.Common.Tools.Enum;
using HIPSClient.Common.Tools.String;
using HealthIdentifiers.Identifiers.Australian.DepartmentVeteransAffairs;
using HealthIdentifiers.Identifiers.Australian.MedicareNumber;
using HealthIdentifiers.Identifiers.Australian.NationalHealthcareIdentifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIPSClient.Hips.Model;

namespace HIPSClient.HipsTinkerTool.ViewModel.Common
{
  public class PathologyReportItemVM : BaseValidationVM
  {
    public IDictionary<string, int> ReportStatusEnumDictionary { get; private set; }
    public PathologyReportItemVM()
    {
      ReportStatusEnumDictionary = EnumUtility.ConvertEnumToUIDisplayDictionary<ResultStatus>();
    }
    
    private string _ReportIdentifier;
    public string ReportIdentifier
    {
      get
      {
        return _ReportIdentifier;
      }
      set
      {
        _ReportIdentifier = value;
        OnPropertyChanged("ReportIdentifier");
      }
    }

    private string _LocalCode;
    public string LocalCode
    {
      get
      {
        return _LocalCode;
      }
      set
      {
        _LocalCode = value;
        OnPropertyChanged("LocalCode");
      }
    }

    private string _LocalDescription;
    public string LocalDescription
    {
      get
      {
        return _LocalDescription;
      }
      set
      {
        _LocalDescription = value;
        OnPropertyChanged("LocalDescription");
      }
    }

    private string _LocalSystemCode;
    public string LocalSystemCode
    {
      get
      {
        return _LocalSystemCode;
      }
      set
      {
        _LocalSystemCode = value;
        OnPropertyChanged("LocalSystemCode");
      }
    }

    private string _SnomedCode;
    public string SnomedCode
    {
      get
      {
        return _SnomedCode;
      }
      set
      {
        _SnomedCode = value;
        OnPropertyChanged("SnomedCode");
        OnPropertyChanged("SnomedPreferredTerm");
      }
    }

    private string _SnomedPreferredTerm;
    public string SnomedPreferredTerm
    {
      get
      {
        return _SnomedPreferredTerm;
      }
      set
      {
        _SnomedPreferredTerm = value;
        OnPropertyChanged("SnomedPreferredTerm");
        OnPropertyChanged("SnomedCode");
      }
    }

    private DateTimeVM _ReportedDateTime;
    public DateTimeVM ReportedDateTime
    {
      get
      {
        return _ReportedDateTime;
      }
      set
      {
        _ReportedDateTime = value;
        OnPropertyChanged("ReportedDateTime");
      }
    }
    
    private string _DepartmentCode;
    public string DepartmentCode
    {
      get
      {
        return _DepartmentCode;
      }
      set
      {
        _DepartmentCode = value;
        OnPropertyChanged("DepartmentCode");
      }
    }
    
    public ResultStatus _ReportStatus { get; private set; }
    public string ReportStatus
    {
      get
      {
        return _ReportStatus.GetUIDisplay(); ;
      }
      set
      {
        _ReportStatus = (ResultStatus)ReportStatusEnumDictionary[value];
        OnPropertyChanged("ReportStatus");
      }
    }

    protected override string IsValid(string PropertyName)
    {      
      if (PropertyName == "ReportIdentifier")
      {
        if (string.IsNullOrWhiteSpace(this._ReportIdentifier))
        {
          AddError("ReportIdentifier", "Report Id must be populated.");
          return "Error Found!";
        }
        else
        {
          RemoveError("ReportIdentifier");
        }
      }

      if (PropertyName == "LocalCode")
      {
        if (string.IsNullOrWhiteSpace(this._LocalCode))
        {
          AddError("LocalCode", "Local Code must be populated.");
          return "Error Found!";
        }
        else
        {
          RemoveError("LocalCode");
        }
      }

      if (PropertyName == "LocalDescription")
      {
        if (string.IsNullOrWhiteSpace(this._LocalDescription))
        {
          AddError("LocalDescription", "Local Desc must be populated.");
          return "Error Found!";
        }
        else
        {
          RemoveError("LocalDescription");
        }
      }

      if (PropertyName == "LocalSystemCode")
      {
        if (string.IsNullOrWhiteSpace(this._LocalSystemCode))
        {
          AddError("LocalSystemCode", "Local System must be populated.");
          return "Error Found!";
        }
        else
        {
          RemoveError("LocalSystemCode");
        }
      }

      if (PropertyName == "SnomedCode")
      {
        if (!string.IsNullOrWhiteSpace(this._SnomedCode) && string.IsNullOrWhiteSpace(this._SnomedPreferredTerm))
        {
          AddError("SnomedCode", "If Snomed Code is populated then Snomed Preferred must also be populated.");
          return "Error Found!";
        }
        else
        {
          RemoveError("SnomedCode");
        }
      }

      if (PropertyName == "SnomedPreferredTerm")
      {
        if (!string.IsNullOrWhiteSpace(this._SnomedPreferredTerm) && string.IsNullOrWhiteSpace(this.SnomedCode))
        {
          AddError("SnomedPreferredTerm", "If Snomed Preferred is populated then Snomed Code must also be populated.");
          return "Error Found!";
        }
        else
        {
          RemoveError("SnomedPreferredTerm");
        }
      }

      if (PropertyName == "DepartmentCode")
      {
        if (string.IsNullOrWhiteSpace(this._DepartmentCode))
        {
          AddError("DepartmentCode", "Department Code must be populated.");
          return "Error Found!";
        }
        else
        {
          RemoveError("DepartmentCode");
        }
      }
      
      return string.Empty;
    }
  }
}
