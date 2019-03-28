using HIPSClient.HipsTinkerTool.Style;
using HIPSClient.HipsTinkerTool.ViewModel.Common;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public class NameWindow : BaseValidationWindow, IView
  {
    public NameVM NameVM { get; private set; }
    public NameVM TempNameVM { get; private set; }

    public NameWindow(NameVM NameVM)
      : base(NameVM)
    {
      this.NameVM = NameVM;
      DataContext = this.NameVM;

      TempNameVM = new NameVM();
      NameVMMapper(NameVM, TempNameVM);

      InitializeLayout();
      Grid MainGrid = GenerateControls();
      MainGrid.Margin = new Thickness(10);
      StackPanel ButtonStack = GenerateButtonStackPanel();
      ButtonStack.HorizontalAlignment = HorizontalAlignment.Right;
      Grid.SetColumn(ButtonStack, 0);
      Grid.SetColumnSpan(ButtonStack, 8);
      Grid.SetRow(ButtonStack, 1);
      MainGrid.Children.Add(ButtonStack);

      Border MainContentBoarder = new Border();      
      MainContentBoarder.BorderBrush = Brushes.CadetBlue;
      MainContentBoarder.BorderThickness = new Thickness(3);
      MainContentBoarder.Child = MainGrid;
      MainContentBoarder.HorizontalAlignment = HorizontalAlignment.Stretch;
      this.SetContent(MainContentBoarder);
    }

    private void NameVMMapper(NameVM From, NameVM To)
    {
      To.Family = From.Family;
      To.Given = From.Given;
      To.Title = From.Title;
    }
    

    public void InitializeLayout()
    {
      Title = "Address";
      Width = 600;
      SizeToContent = SizeToContent.Height;
      ResizeMode = ResizeMode.NoResize;
      WindowStyle = WindowStyle.ToolWindow;
      WindowStartupLocation = WindowStartupLocation.CenterOwner;
      Margin = new Thickness(200);

    }

    protected override void OnClosing(CancelEventArgs e)
    {
      if (this.DialogResult == null || this.DialogResult == false)
      {
        //Copy the unchanged values back as user has cancelled edit.
        NameVMMapper(TempNameVM, NameVM);
        DialogResult = false;
      }
      base.OnClosing(e);
    }


    private Grid GenerateControls()
    {
      int LabelWidth = 45;
      Grid MainGrid = GenerateMainGrid();
      DockPanel Family = GlobalStyleManager.GetValueParameterDockPanel("Family", LabelWidth, "Family");
      Grid.SetColumn(Family, 0);
      Grid.SetColumnSpan(Family, 3);
      Grid.SetRow(Family, 0);
      MainGrid.Children.Add(Family);

      DockPanel Given = GlobalStyleManager.GetValueParameterDockPanel("Given", LabelWidth, "Given");
      Grid.SetColumn(Given, 3);
      Grid.SetColumnSpan(Given, 3);
      Grid.SetRow(Given, 0);
      MainGrid.Children.Add(Given);

      DockPanel Title = GlobalStyleManager.GetValueParameterDockPanel("Title", LabelWidth, "Title");
      Grid.SetColumn(Title, 6);
      Grid.SetColumnSpan(Title, 2);
      Grid.SetRow(Title, 0);
      MainGrid.Children.Add(Title);
      
      return MainGrid;
    }

    private StackPanel GenerateButtonStackPanel()
    {
      Button SaveButton = new Button();
      SaveButton.Content = "Save";
      SaveButton.Width = 60;
      SaveButton.HorizontalAlignment = HorizontalAlignment.Right;
      SaveButton.Margin = new Thickness(2);
      SaveButton.SetBinding(Button.IsEnabledProperty, "CanSave");
      SaveButton.Click += new RoutedEventHandler((obj, e) =>
      {
        DialogResult = true;
        this.Close();
      });

      Button CancelButton = new Button();
      CancelButton.Content = "Cancel";
      CancelButton.Width = 60;
      CancelButton.HorizontalAlignment = HorizontalAlignment.Right;
      CancelButton.Margin = new Thickness(2);
      CancelButton.Click += new RoutedEventHandler((obj, e) =>
      {        
        DialogResult = false;
        this.Close();
      });

      StackPanel ButtonStack = new StackPanel();
      ButtonStack.Orientation = Orientation.Horizontal;
      ButtonStack.Children.Add(CancelButton);
      ButtonStack.Children.Add(SaveButton);
      ButtonStack.HorizontalAlignment = HorizontalAlignment.Right;

      Grid.SetRow(ButtonStack, 1);
      Grid.SetColumn(ButtonStack, 0);

      return ButtonStack;
    }

    private Grid GenerateMainGrid()
    {
      var Col0 = new ColumnDefinition();
      var Col1 = new ColumnDefinition();
      var Col2 = new ColumnDefinition();
      var Col3 = new ColumnDefinition();
      var Col4 = new ColumnDefinition();
      var Col5 = new ColumnDefinition();
      var Col6 = new ColumnDefinition();
      var Col7 = new ColumnDefinition();
      var Col8 = new ColumnDefinition();

      var Row0 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row1 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      
      var Grid = new Grid();
      Grid.HorizontalAlignment = HorizontalAlignment.Stretch;

      Grid.ColumnDefinitions.Add(Col0);
      Grid.ColumnDefinitions.Add(Col1);
      Grid.ColumnDefinitions.Add(Col2);
      Grid.ColumnDefinitions.Add(Col3);
      Grid.ColumnDefinitions.Add(Col4);
      Grid.ColumnDefinitions.Add(Col5);
      Grid.ColumnDefinitions.Add(Col6);
      Grid.ColumnDefinitions.Add(Col7);
      
      Grid.RowDefinitions.Add(Row0);
      Grid.RowDefinitions.Add(Row1);
      
      return Grid;
    }
  }
}
