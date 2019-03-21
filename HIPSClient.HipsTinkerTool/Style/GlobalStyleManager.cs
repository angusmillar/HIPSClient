using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace HIPSClient.HipsTinkerTool.Style
{
  public static class GlobalStyleManager
  {
    public static void SetDefaultButton(Button Button)
    {
      Button.MaxWidth = 75;
      Button.Height = 30;
      Button.Margin = new System.Windows.Thickness(0, 10, 0, 10);
      SetDefaultButtonTheme(Button);
    }

    public static void SetDefaultButtonTheme(Button Button)
    {
      //Button.Background = new SolidColorBrush(Colors.Black);
      //Button.Foreground = new SolidColorBrush(Colors.White);
    }
  }
}
