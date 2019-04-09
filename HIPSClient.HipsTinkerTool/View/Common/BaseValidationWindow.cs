﻿using HIPSClient.HipsTinkerTool.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public abstract class BaseValidationWindow : Window
  {

    private ValidationTool ValTool;
    public BaseValidationWindow(BaseValidationVM BaseValidationVM)
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
