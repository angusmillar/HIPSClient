using System;
using System.Globalization;

namespace HIPSClient.HipsTinkerTool.ViewModel.Common
{
  public class OrderVM : BaseValidationVM
  {
    public OrderVM()
    {
      ProviderName = new NameVM();
    }

    private string _OrderNumber;
    public string OrderNumber
    {
      get
      {
        return _OrderNumber;
      }
      set
      {
        _OrderNumber = value;
        OnPropertyChanged("OrderNumber");
      }
    }
    public NameVM ProviderName { get; set; }
    public DateTimeVM RequestedDateTime { get; set; }
    public DateTimeVM CollectionDateTime { get; set; }
    private bool _IsMyHealthRecordDisclosed;
    public bool IsMyHealthRecordDisclosed
    {
      get
      {
        return _IsMyHealthRecordDisclosed;
      }
      set
      {
        _IsMyHealthRecordDisclosed = value;
        OnPropertyChanged("IsMyHealthRecordDisclosed");
      }
    }

    protected override string IsValid(string PropertyName)
    {
      //if (PropertyName == "RequestedTimeString")
      //{
      //  if (TryParseTime(this.RequestedTimeString) == null)
      //  {
      //    AddError("RequestedTimeString", "Time must be formatted as hh:mm PM/ AM(e.g. 9:30 PM)");
      //    return "Error Found";
      //  }
      //  else
      //  {
      //    RemoveError("RequestedTimeString");
      //  }        
      //}

      return string.Empty;
    }
  }
}
