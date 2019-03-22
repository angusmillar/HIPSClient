using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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

    public static DockPanel GetValueComboBoxEnumDockPanel(string LabelName, int LabelWidth, string ValueBindingName, List<string> ItemsSource)
    {
      Label ValueLabel = new Label();
      ValueLabel.Content = LabelName;
      ValueLabel.Width = LabelWidth;
      ValueLabel.VerticalContentAlignment = VerticalAlignment.Center;
      ValueLabel.FontWeight = FontWeights.DemiBold;
      ValueLabel.Margin = new Thickness(3);
      DockPanel.SetDock(ValueLabel, Dock.Left);

      Binding Binding = new Binding(ValueBindingName);
      Binding.ValidationRules.Clear();
      Binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
      Binding.NotifyOnValidationError = true;
      Binding.ValidatesOnDataErrors = true;

      ComboBox IdTypeComoBox = new ComboBox();
      IdTypeComoBox.ItemsSource = ItemsSource;
      IdTypeComoBox.SetBinding(ComboBox.SelectedItemProperty, Binding);
      IdTypeComoBox.VerticalContentAlignment = VerticalAlignment.Center;
      IdTypeComoBox.Margin = new Thickness(3);
      DockPanel.SetDock(IdTypeComoBox, Dock.Left);
      
      DockPanel Panel = new DockPanel();
      Panel.LastChildFill = true;
      Panel.HorizontalAlignment = HorizontalAlignment.Stretch;
      Panel.Children.Add(ValueLabel);
      Panel.Children.Add(IdTypeComoBox);

      return Panel;
    }

    public static DockPanel GetValueParameterDockPanel(string LabelName, int LabelWidth, string ValueBindingName, bool IsReadOnly = false)
    {
      Label ValueLabel = new Label();
      ValueLabel.Content = LabelName;
      ValueLabel.Width = LabelWidth;
      ValueLabel.VerticalContentAlignment = VerticalAlignment.Center;
      ValueLabel.FontWeight = FontWeights.DemiBold;
      ValueLabel.Margin = new Thickness(3);
      DockPanel.SetDock(ValueLabel, Dock.Left);

      Binding Binding = new Binding(ValueBindingName);
      Binding.ValidationRules.Clear();
      Binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
      Binding.NotifyOnValidationError = true;
      Binding.ValidatesOnDataErrors = true;

      TextBox ValueTextBox = new TextBox();
      ValueTextBox.SetBinding(TextBox.TextProperty, Binding);
      ValueTextBox.VerticalContentAlignment = VerticalAlignment.Center;
      ValueTextBox.Margin = new Thickness(3);
      ValueTextBox.IsReadOnly = IsReadOnly;
      DockPanel.SetDock(ValueTextBox, Dock.Left);

      DockPanel Panel = new DockPanel();
      Panel.LastChildFill = true;
      Panel.HorizontalAlignment = HorizontalAlignment.Stretch;
      Panel.Children.Add(ValueLabel);
      Panel.Children.Add(ValueTextBox);
      return Panel;
    }
  }
}
