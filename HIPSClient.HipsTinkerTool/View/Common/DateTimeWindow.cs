using HIPSClient.HipsTinkerTool.Style;
using HIPSClient.HipsTinkerTool.ViewModel.Common;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public class DateTimeWindow : BaseValidationWindow, IView
  {
    public DateTimeVM DateTimeVM { get; private set; }
    public DateTimeVM TempDateTimeVM { get; private set; }    

    public DateTimeWindow(DateTimeVM DateTimeVM)
      : base(DateTimeVM)
    {
      this.DateTimeVM = DateTimeVM;
      DataContext = this.DateTimeVM;

      TempDateTimeVM = new DateTimeVM();
      DateTimeVMMapper(DateTimeVM, TempDateTimeVM);

      InitializeLayout();
      Grid MainGrid = GenerateMainGrid();
      MainGrid.Margin = new Thickness(10);
      GenerateControls(MainGrid);
      StackPanel ButtonStack = GenerateButtonStackPanel();
      Grid.SetColumn(ButtonStack, 0);
      Grid.SetColumnSpan(ButtonStack, 8);
      Grid.SetRow(ButtonStack, 2);
      MainGrid.Children.Add(ButtonStack);

      Border MainContentBoarder = new Border();
      //MainContentBoarder.Background = Brushes.GhostWhite;
      MainContentBoarder.BorderBrush = Brushes.CadetBlue;
      MainContentBoarder.BorderThickness = new Thickness(3);
      MainContentBoarder.Child = MainGrid;
      MainContentBoarder.HorizontalAlignment = HorizontalAlignment.Stretch;
      this.SetContent(MainContentBoarder);
    }

    private void DateTimeVMMapper(DateTimeVM From, DateTimeVM To)
    {
      To.Date = From.Date;
      To.TimeZone = From.TimeZone;
      To.TimeFormated = From.TimeFormated;

    }


    public void InitializeLayout()
    {
      Title = " Edit Date & Time";
      Width = 500;
      ResizeMode = ResizeMode.NoResize;
      WindowStyle = WindowStyle.ToolWindow;
      WindowStartupLocation = WindowStartupLocation.CenterOwner;
      SizeToContent = SizeToContent.Height;
    }

    private void GenerateControls(Grid MainGrid)
    {
      DockPanel RequestedDate = GlobalStyleManager.GetValueDatePickerDockPanel("Date", 45, "Date");
      Grid.SetColumn(RequestedDate, 0);
      Grid.SetColumnSpan(RequestedDate, 3);
      Grid.SetRow(RequestedDate, 1);
      MainGrid.Children.Add(RequestedDate);

      DockPanel RequestedTime = GlobalStyleManager.GetValueParameterDockPanel("Time", 45, "TimeFormated");
      Grid.SetColumn(RequestedTime, 3);
      Grid.SetColumnSpan(RequestedTime, 3);
      Grid.SetRow(RequestedTime, 1);
      MainGrid.Children.Add(RequestedTime);

    }

    protected override void OnClosing(CancelEventArgs e)
    {
      if (this.DialogResult == null || this.DialogResult == false)
      {
        DateTimeVMMapper(TempDateTimeVM, DateTimeVM);
        DialogResult = false;
      }

      base.OnClosing(e);
    }

    private StackPanel GenerateButtonStackPanel()
    {
      Button SavePatientIdButton = new Button();
      SavePatientIdButton.Content = "Save";
      SavePatientIdButton.Width = 60;
      SavePatientIdButton.HorizontalAlignment = HorizontalAlignment.Right;
      SavePatientIdButton.Margin = new Thickness(2);
      SavePatientIdButton.SetBinding(Button.IsEnabledProperty, "CanSave");
      SavePatientIdButton.Click += new RoutedEventHandler((obj, e) =>
      {
        DialogResult = true;
        this.Close();
      });

      Button CancelPatientIdButton = new Button();
      CancelPatientIdButton.Content = "Cancel";
      CancelPatientIdButton.Width = 60;
      CancelPatientIdButton.HorizontalAlignment = HorizontalAlignment.Right;
      CancelPatientIdButton.Margin = new Thickness(2);
      CancelPatientIdButton.Click += new RoutedEventHandler((obj, e) =>
      {
        //Copy the unchanged values back as user has cancelled edit.
        //DateTimeVMMapper(TempDateTimeVM, DateTimeVM);        
        DialogResult = false;
        this.Close();
      });

      StackPanel ButtonStack = new StackPanel();
      ButtonStack.Orientation = Orientation.Horizontal;
      ButtonStack.Children.Add(CancelPatientIdButton);
      ButtonStack.Children.Add(SavePatientIdButton);
      ButtonStack.HorizontalAlignment = HorizontalAlignment.Right;

      //Grid.SetRow(ButtonStack, 4);
      //Grid.SetColumn(ButtonStack, 0);

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

      var Row0 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row1 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row2 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row3 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row4 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row5 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row6 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row7 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };


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
      Grid.RowDefinitions.Add(Row2);
      //Grid.RowDefinitions.Add(Row3);
      //Grid.RowDefinitions.Add(Row4);
      //Grid.RowDefinitions.Add(Row5);
      //Grid.RowDefinitions.Add(Row6);
      //Grid.RowDefinitions.Add(Row7);      

      return Grid;
    }
  }
}
