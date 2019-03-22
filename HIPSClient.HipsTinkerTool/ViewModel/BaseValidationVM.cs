using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HIPSClient.HipsTinkerTool.ViewModel
{
  public abstract class BaseValidationVM : BaseVM, IDataErrorInfo, INotifyPropertyChanged
  {
    public BaseValidationVM()
    {
      _ErrorDic = new Dictionary<string, string>();
    }
    
    //The list of current errors
    protected Dictionary<string, string> _ErrorDic;
    protected void AddError(string Type, string Message)
    {
      if (_ErrorDic.ContainsKey(Type))
      {
        _ErrorDic[Type] = Message;
      }
      else
      {
        _ErrorDic.Add(Type, Message);
      }
      OnPropertyChanged("ErrorMessage");
    }
    protected void RemoveError(string Type)
    {
      if (_ErrorDic.ContainsKey(Type))
      {
        _ErrorDic.Remove(Type);
      }
      OnPropertyChanged("ErrorMessage");
    }
    protected void ClearAllErrors()
    {
      _ErrorDic.Clear();
      OnPropertyChanged("ErrorMessage");
    }

    //The error message for the error reporting control to bind to.
    //Binding must be oneway and AddError, RemoveError, ClearAllErrors raise the OnChnageEvent
    public string ErrorMessage
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        foreach (var Item in _ErrorDic)
        {
          sb.AppendLine(Item.Value);
        }
        return sb.ToString();
      }
    }

    public string this[string PropertyName]
    {
      get
      {
        return IsValid(PropertyName);
      }
    }

    //We override this to do the validation in the inherited class
    //if not overridden then IsValid is always true.
    protected virtual string IsValid(string PropertyName)
    {
      return string.Empty;
    }

    //Not Used but required by IDataErrorInfo interface
    public string Error => throw new NotImplementedException();
  }
}
