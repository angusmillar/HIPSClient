using HIPSClient.HipsTinkerTool.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public class ValidationTool 
  {
    public int _ErrorCount;
    public DockPanel _MainDocK;
    public TextBox _ErrorMessageTextBox;
    public Border _ErrorBorder;
    public BaseValidationVM BaseValidationVM { get; private set; }

    public ValidationTool(BaseValidationVM BaseValidationVM)
    {
      this.BaseValidationVM = BaseValidationVM;
      _ErrorCount = 0;
      InitializeMainDock();
    }

    public void SetContent(UIElement Item)
    {
      _MainDocK.Children.Clear();
      DockPanel.SetDock(Item, Dock.Top);
      DockPanel.SetDock(_ErrorBorder, Dock.Bottom);
      _MainDocK.Children.Add(Item);
      _MainDocK.Children.Add(_ErrorBorder);
    }

    public DockPanel GetContent()
    {
      return _MainDocK;
    }

    private void InitializeMainDock()
    {
      //Create a Dock as the main Windows control and the dock has two
      //Controls.
      // - At the top the Main Grid for all content
      // - At the bottom a Border with a text bock inside for error messages when present.
      _MainDocK = new DockPanel();
      _MainDocK.LastChildFill = true;

      GenerateErrorMessageDisplay();           
    }

    private void GenerateErrorMessageDisplay()
    {
      _ErrorMessageTextBox = new TextBox();
      _ErrorMessageTextBox.VerticalAlignment = VerticalAlignment.Stretch;
      _ErrorMessageTextBox.HorizontalAlignment = HorizontalAlignment.Stretch;
      _ErrorMessageTextBox.BorderThickness = new Thickness(0);
      _ErrorMessageTextBox.AcceptsReturn = true;
      _ErrorMessageTextBox.IsReadOnly = true;
      _ErrorMessageTextBox.Background = Brushes.Salmon;
      _ErrorMessageTextBox.TextWrapping = TextWrapping.Wrap;
      _ErrorMessageTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
      

      Binding Bind = new Binding("ErrorMessage");
      Bind.Mode = BindingMode.OneWay;
      _ErrorMessageTextBox.SetBinding(TextBox.TextProperty, Bind);

      _ErrorBorder = new Border();
      _ErrorBorder.Background = Brushes.GhostWhite;
      _ErrorBorder.BorderBrush = Brushes.Red;
      _ErrorBorder.BorderThickness = new Thickness(1);
      _ErrorBorder.Child = _ErrorMessageTextBox;
      _ErrorBorder.Visibility = Visibility.Collapsed;
      DockPanel.SetDock(_ErrorBorder, System.Windows.Controls.Dock.Bottom);
    }

    public void OnErrorEvent(object sender, RoutedEventArgs e)
    {
      var validationEventArgs = e as ValidationErrorEventArgs;
      if (validationEventArgs == null)
      {
        throw new Exception("Unexpected ValidationErrorEventArgs equals null");
      }
      var x = validationEventArgs.Error;
      switch (validationEventArgs.Action)
      {
        case ValidationErrorEventAction.Added:
          _ErrorCount++;
          break;
        case ValidationErrorEventAction.Removed:
          _ErrorCount--;
          break;
        default:
          throw new Exception($"Unexpected Action of {validationEventArgs.Action.ToString()}");
      }

      //Set the Save button visibility based on error count
      //BaseValidationVM.CanSave = _ErrorCount == 0;

      //Hide the Error Message display.
      if (_ErrorCount == 0)
      {
        _ErrorBorder.Visibility = Visibility.Collapsed;
      }
      else
      {
        _ErrorBorder.Visibility = Visibility.Visible;
      }
    }
  }
}
