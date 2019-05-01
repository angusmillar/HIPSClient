using HIPSClient.HipsTinkerTool.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public class ValidationTool
  {
    public int _ErrorCount;
    public Grid _MainGrid;
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
      _MainGrid.Children.Clear();

      Grid.SetRow(Item, 0);
      Grid.SetColumn(Item, 0);
      _MainGrid.Children.Add(Item);

      Grid.SetRow(_ErrorBorder, 1);
      Grid.SetColumn(_ErrorBorder, 0);
      _MainGrid.Children.Add(_ErrorBorder);
    }

    public Grid GetContent()
    {
      return _MainGrid;
    }

    private void InitializeMainDock()
    {
      //Create a Dock as the main Windows control and the dock has two
      //Controls.
      // - At the top the Main Grid for all content
      // - At the bottom a Border with a text bock inside for error messages when present.
      _MainGrid = new Grid();

      var Row1 = new RowDefinition();
      Row1.Height = new GridLength(30, GridUnitType.Star);
      _MainGrid.RowDefinitions.Add(Row1);

      var Row2 = new RowDefinition();
      Row2.Height = new GridLength(1, GridUnitType.Star);
      _MainGrid.RowDefinitions.Add(Row2);

      var Col1 = new ColumnDefinition();
      _MainGrid.ColumnDefinitions.Add(Col1);

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

      Grid.SetRow(_ErrorBorder, 1);
      Grid.SetColumn(_ErrorBorder, 0);
      _MainGrid.Children.Add(_ErrorBorder);
      
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
