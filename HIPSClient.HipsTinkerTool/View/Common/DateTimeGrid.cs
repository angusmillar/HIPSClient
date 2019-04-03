using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using HIPSClient.HipsTinkerTool.Style;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public class DateTimeGrid : Grid, IView
  {
    public DateTimeGrid(int LabelWidth= 45)
    {
      this.LabelWidth = LabelWidth;
      InitializeLayout();
      GenerateControls();
    }

    private int LabelWidth { get; set; }

    private void GenerateControls()
    {
      DockPanel Date = GlobalStyleManager.GetValueDatePickerDockPanel("Date", LabelWidth, "Date");
      Grid.SetColumn(Date, 0);
      Grid.SetColumnSpan(Date, 3);
      Grid.SetRow(Date, 1);
      this.Children.Add(Date);

      DockPanel Time = GlobalStyleManager.GetValueParameterDockPanel("Time", 45, "TimeFormatted");
      Grid.SetColumn(Time, 3);
      Grid.SetColumnSpan(Time, 3);
      Grid.SetRow(Time, 1);
      this.Children.Add(Time);

      DockPanel TimeZone = GlobalStyleManager.GetValueParameterDockPanel("Zone", 45, "TimeZoneFormatted");
      Grid.SetColumn(TimeZone, 6);
      Grid.SetColumnSpan(TimeZone, 3);
      Grid.SetRow(TimeZone, 1);
      this.Children.Add(TimeZone);
    }

    public void InitializeLayout()
    {
      this.SetGrid(3, 8);
    }
  }
}
