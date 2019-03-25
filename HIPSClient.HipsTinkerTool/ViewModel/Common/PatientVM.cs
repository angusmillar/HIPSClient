using HIPSClient.Common.Tools.Enum;
using System;
using System.Collections.Generic;
using HIPSClient.Hips.Model;

namespace HIPSClient.HipsTinkerTool.ViewModel.Common
{
  public class PatientVM : BaseValidationVM
  {
    public IDictionary<string, int> GenderEnumDictionary { get; private set; }
    public PatientVM()
    {
      GenderEnumDictionary = EnumUtility.ConvertEnumToUIDisplayDictionary<Gender>();
      _Address = new AddressVM();
    }

    private string _FamilyName;
    public string FamilyName
    {
      get
      {
        return _FamilyName;
      }
      set
      {
        _FamilyName = value;
        OnPropertyChanged("FamilyName");
      }
    }

    private string _GivenName;
    public string GivenName
    {
      get
      {
        return _GivenName;
      }
      set
      {
        _GivenName = value;
        OnPropertyChanged("GivenName");
      }
    }

    private string _Title;
    public string Title
    {
      get
      {
        return _Title;
      }
      set
      {
        _Title = value;
        OnPropertyChanged("Title");
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
        OnPropertyChanged("Address");
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

    protected override string IsValid(string PropertyName)
    {

      if (PropertyName == "FamilyName")
      {
        if (string.IsNullOrWhiteSpace(this.FamilyName))
        {
          AddError("FamilyName", $"Family can not be empty.");
          return "Error Found!";
        }
        RemoveError("FamilyName");
      }

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
