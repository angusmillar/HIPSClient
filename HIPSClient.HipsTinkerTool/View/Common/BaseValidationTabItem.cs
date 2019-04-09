using HIPSClient.HipsTinkerTool.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public class BaseValidationTabItem : TabItem
  {
    private ValidationTool ValTool;
    public BaseValidationTabItem(BaseValidationVM BaseValidationVM)
    {
      ValTool = new ValidationTool(BaseValidationVM);
      AddHandler(Validation.ErrorEvent, new RoutedEventHandler(OnErrorEvent));
      this.Content = ValTool.GetContent();
    }

    protected void SetContent(UIElement Item)
    {
      ValTool.SetContent(Item);
    }

    private void OnErrorEvent(object sender, RoutedEventArgs e)
    {
      ValTool.OnErrorEvent(sender, e);      
    }
  }
}
