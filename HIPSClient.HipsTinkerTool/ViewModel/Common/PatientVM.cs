using HIPSClient.Common.Tools.Enum;
using System;
using System.Collections.Generic;
using HIPSClient.Hips.Model;

namespace HIPSClient.HipsTinkerTool.ViewModel.Common
{
  public class PatientVM : BaseValidationVM
  {
    public IDictionary<string, int> GenderEnumDictionary { get; private set; }
    public IDictionary<string, int> IndigenousStatusEnumDictionary { get; private set; }
    public PatientVM()
    {
      GenderEnumDictionary = EnumUtility.ConvertEnumToUIDisplayDictionary<Gender>();
      IndigenousStatusEnumDictionary = EnumUtility.ConvertEnumToUIDisplayDictionary<IndigenousStatusType>();
      _PatientName = new NameVM();
      _Address = new AddressVM();
    }

    private NameVM _PatientName;
    public NameVM PatientName
    {
      get
      {
        return _PatientName;
      }
      set
      {
        _PatientName = value;       
      }
    }
    
    private Gender _Gender;
    public string Gender
    {
      get
      {
        return _Gender.GetUIDisplay();
      }
      set
      {        
        _Gender = (Gender)GenderEnumDictionary[value];
        OnPropertyChanged("Gender");
      }
    }

    private DateTime? _DateOfBirth;
    public DateTime? DateOfBirth
    {
      get { return _DateOfBirth; }
      set
      {
        _DateOfBirth = value;
        OnPropertyChanged("DateOfBirth");       
      }
    }
   
    private AddressVM _Address;
    public AddressVM Address
    {
      get
      {
        return _Address;
      }
      set
      {
        _Address = value;       
      }
    }

    private string _HomePhone;
    public string HomePhone
    {
      get
      {
        return _HomePhone;
      }
      set
      {
        _HomePhone = value;
        OnPropertyChanged("HomePhone");
      }
    }

    private string _WorkPhone;
    public string WorkPhone
    {
      get
      {
        return _WorkPhone;
      }
      set
      {
        _WorkPhone = value;
        OnPropertyChanged("WorkPhone");
      }
    }

    private IndigenousStatusType _IndigenousStatus;
    public string IndigenousStatus
    {
      get
      {
        return _IndigenousStatus.GetUIDisplay();
      }
      set
      {
        _IndigenousStatus = (IndigenousStatusType)IndigenousStatusEnumDictionary[value];
        OnPropertyChanged("IndigenousStatus");
      }
    }

    protected override string IsValid(string PropertyName)
    {
      if (PropertyName == "DateOfBirth")
      {
        if (!this.DateOfBirth.HasValue)
        {
          AddError("DateOfBirth", $"Date of Birth (DOB) must be populated.");
          return "Error Found!";
        }
        RemoveError("DateOfBirth");
      }

      return string.Empty;
    }
  }
}
