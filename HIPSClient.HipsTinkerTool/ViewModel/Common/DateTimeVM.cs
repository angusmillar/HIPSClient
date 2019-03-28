using System;
using System.Globalization;

namespace HIPSClient.HipsTinkerTool.ViewModel.Common
{
  public class DateTimeVM : BaseValidationVM
  {
    private DateTime? _Date;
    public DateTime? Date
    {
      get { return _Date; }
      set
      {
        _Date = value;
        OnPropertyChanged("Date");
        OnPropertyChanged("DateTimeFormatted");
      }
    }

    public DateTime? _Time;
    private string _TimeFormated;
    public string TimeFormated
    {
      get
      {
        if (_Time.HasValue)
          return _Time.Value.ToString("h:mm tt");
        else
          return _TimeFormated;
      }
      set
      {
        _TimeFormated = value;
        _Time = TryParseTime(value);
        OnPropertyChanged("TimeFormated");
        OnPropertyChanged("DateTimeFormatted");
      }
    }

    private string _TimeZone;
    public string TimeZone
    {
      get { return _TimeZone; }
      set
      {
        _TimeZone = value;
        OnPropertyChanged("TimeZone");
        OnPropertyChanged("DateTimeFormatted");
      }
    }


    public string DateTimeFormatted
    {
      get
      {
        return $"{Date.Value.ToString("dd-MMM-yyyy")} {TimeFormated} +10:00";
      }  
    }


    private DateTime? TryParseTime(string TimeString)
    {
      DateTime TempDateTime;
      if (DateTime.TryParseExact(TimeString, "h:mm tt", System.Globalization.DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowWhiteSpaces, out TempDateTime))
      {
        return TempDateTime;
      }
      else
      {
        return null;
      }
    }

    protected override string IsValid(string PropertyName)
    {
      if (PropertyName == "TimeFormated")
      {
        if (TryParseTime(this.TimeFormated) == null)
        {
          AddError("TimeFormated", "Time must be formatted as hh:mm PM/ AM(e.g. 9:30 PM)");
          return "Error Found";
        }
        else
        {
          RemoveError("TimeFormated");
        }
      }

      return string.Empty;
    }


  }
}
