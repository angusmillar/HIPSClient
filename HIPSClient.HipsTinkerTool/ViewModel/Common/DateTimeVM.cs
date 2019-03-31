using System;
using System.Globalization;
using HIPSClient.Common.Tools.String;

namespace HIPSClient.HipsTinkerTool.ViewModel.Common
{
  public class DateTimeVM : BaseValidationVM
  {
    public DateTimeVM()
    {
      IsTimeOptional = false;
    }

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

    public bool IsTimeOptional { get; set; }

    public DateTime? _Time;
    private string _TimeFormatted;
    public string TimeFormatted
    {
      get
      {
        if (_Time.HasValue)
          return _Time.Value.ToString("h:mm tt");
        else
          return _TimeFormatted;
      }
      set
      {
        _TimeFormatted = value;
        _Time = TryParseTime(value);
        OnPropertyChanged("TimeFormatted");
        OnPropertyChanged("TimeZoneFormatted");
        OnPropertyChanged("DateTimeFormatted");
      }
    }

    public TimeSpan? _TimeZone;
    private string _TimeZoneFormatted;
    public string TimeZoneFormatted
    {
      get
      {
        if (_TimeZone.HasValue)
        {
          if (_TimeZone.Value.Hours < 0)
            return _TimeZone.Value.ToString(@"\-hhmm");
          else
            return _TimeZone.Value.ToString(@"\+hhmm");
        }          
        else
        {
          return _TimeZoneFormatted;
        }
          
      }
      set
      {
        _TimeZoneFormatted = value;
        _TimeZone = TryParseTimeZone(value);        
        OnPropertyChanged("TimeZoneFormatted");
        OnPropertyChanged("TimeFormatted");
        OnPropertyChanged("DateTimeFormatted");
      }
    }

    public string DateTimeFormatted
    {
      get
      {
        if (Date.HasValue)
        {
          return $"{Date.Value.ToString("dd-MMM-yyyy")} {TimeFormatted} {TimeZoneFormatted}";
        }
        else
        {
          return $"{TimeFormatted} {TimeZoneFormatted}";
        }

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

    private TimeSpan? TryParseTimeZone(string TimeSpanString)
    {
      string temp = TimeSpanString.RemoveWhitespace();
      if (temp.StartsWith("-"))
      {
        temp = temp.TrimStart('-');
        var culture = CultureInfo.CurrentCulture;
        TimeSpan TempTimeSpan;
        if (TimeSpan.TryParseExact(temp, @"hhmm", culture, TimeSpanStyles.AssumeNegative, out TempTimeSpan))     
        {          
          return TempTimeSpan;
        }
        else
        {
          return null;
        }
      }
      else if (temp.StartsWith("+"))
      {
        temp = temp.TrimStart('+');
        var culture = CultureInfo.CurrentCulture;
        TimeSpan TempTimeSpan;
        if (TimeSpan.TryParseExact(temp, @"hhmm", culture, TimeSpanStyles.None, out TempTimeSpan))
        {
          return TempTimeSpan;
        }
        else
        {
          return null;
        }
      }
      else
      {
        return null;
      }


    }


    protected override string IsValid(string PropertyName)
    {
      if (PropertyName == "Date")
      {
        if (!this.Date.HasValue)
        {
          AddError("Date", "Date must be populated.");
          return "Error Found";
        }
        else
        {
          RemoveError("Date");
        }
      }

      if (PropertyName == "TimeFormatted")
      {
        if (IsTimeOptional && string.IsNullOrWhiteSpace(this.TimeFormatted))
        {
          if (!string.IsNullOrWhiteSpace(this.TimeZoneFormatted))
          {
            AddError("TimeFormatted", "If no Time is provided then Zone should also be empty");
            return "Error Found";
          }
          else
          {
            RemoveError("TimeFormatted");
            return string.Empty;
          }          
        }
        else
        {
          if (TryParseTime(this.TimeFormatted) == null)
          {
            AddError("TimeFormatted", "Time must be formatted as hh:mm AM/PM (e.g. 9:30 PM)");
            return "Error Found";
          }
          else
          {
            RemoveError("TimeFormatted");
          }
        } 
      }

      if (PropertyName == "TimeZoneFormatted")
      {
        if (IsTimeOptional && string.IsNullOrWhiteSpace(this.TimeZoneFormatted))
        {
          if (!string.IsNullOrWhiteSpace(this.TimeFormatted))
          {
            AddError("TimeZoneFormatted", "If no Zone is provided then Time should also be empty");
            return "Error Found";
          }
          else
          {
            RemoveError("TimeZoneFormatted");
            return string.Empty;
          }
        }
        else
        {
          if (TryParseTimeZone(this.TimeZoneFormatted) == null)
          {
            AddError("TimeZoneFormatted", "TimeZone must be formatted as -/+hhmm (e.g. +1000 or -0800)");
            return "Error Found";
          }
          else
          {
            RemoveError("TimeZoneFormatted");
          }
        }
      }

      return string.Empty;
    }


  }
}
