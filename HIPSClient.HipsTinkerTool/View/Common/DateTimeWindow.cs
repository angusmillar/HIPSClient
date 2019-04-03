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
    
    public bool IsTimeOptional
    {
      get
      {
        return DateTimeVM.IsTimeOptional;
      }
      set
      {
        DateTimeVM.IsTimeOptional = value;
      }
    }

    public DateTimeWindow(DateTimeVM DateTimeVM)
      : base(DateTimeVM)
    {
      this.DateTimeVM = DateTimeVM;
      DataContext = this.DateTimeVM;

      TempDateTimeVM = new DateTimeVM();
      DateTimeVMMapper(DateTimeVM, TempDateTimeVM);

      InitializeLayout();

      DateTimeGrid MainGrid = new DateTimeGrid();

      //Grid MainGrid = GenerateMainGrid();
      MainGrid.Margin = new Thickness(10);
      //GenerateControls(MainGrid);
      StackPanel ButtonStack = GenerateButtonStackPanel();
      Grid.SetColumn(ButtonStack, 0);
      Grid.SetColumnSpan(ButtonStack, 8);
      Grid.SetRow(ButtonStack, 2);
      MainGrid.Children.Add(ButtonStack);

      Border MainContentBoarder = new Border();    
      MainContentBoarder.BorderBrush = Brushes.CadetBlue;
      MainContentBoarder.BorderThickness = new Thickness(3);
      MainContentBoarder.Child = MainGrid;
      MainContentBoarder.HorizontalAlignment = HorizontalAlignment.Stretch;
      this.SetContent(MainContentBoarder);
    }

    private void DateTimeVMMapper(DateTimeVM From, DateTimeVM To)
    {
      To.Date = From.Date;
      To.TimeZoneFormatted = From.TimeZoneFormatted;
      To.TimeFormatted = From.TimeFormatted;      
    }

    public void InitializeLayout()
    {
      Title = " Edit Date & Time";
      Width = 600;
      ResizeMode = ResizeMode.NoResize;
      WindowStyle = WindowStyle.ToolWindow;
      WindowStartupLocation = WindowStartupLocation.CenterOwner;
      SizeToContent = SizeToContent.Height;
    }

    //private void GenerateControls(Grid MainGrid)
    //{
    //  DockPanel Date = GlobalStyleManager.GetValueDatePickerDockPanel("Date", 45, "Date");
    //  Grid.SetColumn(Date, 0);
    //  Grid.SetColumnSpan(Date, 3);
    //  Grid.SetRow(Date, 1);
    //  MainGrid.Children.Add(Date);

    //  DockPanel Time = GlobalStyleManager.GetValueParameterDockPanel("Time", 45, "TimeFormatted");
    //  Grid.SetColumn(Time, 3);
    //  Grid.SetColumnSpan(Time, 3);
    //  Grid.SetRow(Time, 1);
    //  MainGrid.Children.Add(Time);

    //  DockPanel TimeZone = GlobalStyleManager.GetValueParameterDockPanel("Zone", 45, "TimeZoneFormatted");
    //  Grid.SetColumn(TimeZone, 6);
    //  Grid.SetColumnSpan(TimeZone, 3);
    //  Grid.SetRow(TimeZone, 1);
    //  MainGrid.Children.Add(TimeZone);

    //}

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
        DialogResult = false;
        this.Close();
      });

      StackPanel ButtonStack = new StackPanel();
      ButtonStack.Orientation = Orientation.Horizontal;
      ButtonStack.Children.Add(CancelPatientIdButton);
      ButtonStack.Children.Add(SavePatientIdButton);
      ButtonStack.HorizontalAlignment = HorizontalAlignment.Right;
      
      return ButtonStack;
    }

    private Grid GenerateMainGrid()
    {
      return GlobalStyleManager.GetGrid(3, 8);      
    }
  }
}
