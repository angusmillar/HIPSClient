using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.HipsTinkerTool.ViewModel.Common
{
  public class AddressVM : BaseValidationVM
  {
    private string _AddressLineOne;
    public string AddressLineOne
    {
      get
      {
        return _AddressLineOne;
      }
      set
      {
        _AddressLineOne = value;
        OnPropertyChanged("AddressLineOne");
        OnPropertyChanged("Suburb");
        OnPropertyChanged("AddressFormated");
      }
    }

    private string _AddressLineTwo;
    public string AddressLineTwo
    {
      get
      {
        return _AddressLineTwo;
      }
      set
      {
        _AddressLineTwo = value;
        OnPropertyChanged("AddressLineTwo");
        OnPropertyChanged("Suburb");
        OnPropertyChanged("AddressFormated");
      }
    }

    private string _Suburb;
    public string Suburb
    {
      get
      {
        return _Suburb;
      }
      set
      {
        _Suburb = value;
        OnPropertyChanged("Suburb");
        OnPropertyChanged("AddressFormated");
      }
    }

    private string _PostCode;
    public string PostCode
    {
      get
      {
        return _PostCode;
      }
      set
      {
        _PostCode = value;
        OnPropertyChanged("PostCode");
        OnPropertyChanged("AddressFormated");
      }
    }

    private string _State;
    public string State
    {
      get
      {
        return _State;
      }
      set
      {
        _State = value;
        OnPropertyChanged("State");
        OnPropertyChanged("AddressFormated");
      }
    }

    private string _Country;
    public string Country
    {
      get
      {
        return _Country;
      }
      set
      {
        _Country = value;
        OnPropertyChanged("Country");
        OnPropertyChanged("AddressFormated");
      }
    }

    public string AddressFormated
    {
      get
      {
        return $"{this.AddressLineOne}, {this.AddressLineTwo}, {this.Suburb}, {this.PostCode}, {this.State}, {this.Country}";
      }
    }
  
    protected override string IsValid(string PropertyName)
    {

      //if (PropertyName == "AddressLineOne")
      //{
      //  if (string.IsNullOrWhiteSpace(this.Suburb))
      //  {
      //    AddError("AddressLineOne", $"If you have Line One then must have suburb");
      //    return "Error Found!";
      //  }
      //  RemoveError("AddressLineOne");
      //}

      if (PropertyName == "Suburb")
      {
        if (!string.IsNullOrWhiteSpace(this.AddressLineOne) || !string.IsNullOrWhiteSpace(this.AddressLineTwo))
        {
          if (string.IsNullOrWhiteSpace(this.Suburb))
          {
            AddError("Suburb", $"If Line One or Line Two is populated then Suburb must be populated.");
            return "Error Found!";
          }          
        }
        RemoveError("Suburb");
      }


      return string.Empty;
    }
  }
}
