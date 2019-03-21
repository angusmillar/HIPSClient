using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.HipsTinkerTool.ViewModel.Pathology
{
  public class PatientIdentifierItemVM : BaseVM
  {
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

    private string _Type;
    public string Type
    {
      get
      {
        return _Type;
      }
      set
      {
        _Type = value;
        OnPropertyChanged("Type");
      }
    }

  }
}
