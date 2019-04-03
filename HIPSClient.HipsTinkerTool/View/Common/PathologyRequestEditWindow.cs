using HIPSClient.Common.Tools.String;
using HIPSClient.HipsTinkerTool.Style;
using HIPSClient.HipsTinkerTool.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public class PathologyRequestEditWindow : BaseValidationWindow, IView
  {
    public PathologyRequestItemVM PathologyRequestItem { get; private set; }
    public PathologyRequestItemVM TempPathologyRequestItem { get; private set; }
        
    public PathologyRequestEditWindow(PathologyRequestItemVM PathologyRequestItem) 
      : base(PathologyRequestItem)
    {
      this.PathologyRequestItem = PathologyRequestItem;
      
      //This is a copy of the unchanged data in case they cancel the edit operation
      TempPathologyRequestItem = new PathologyRequestItemVM();
      PathologyRequestItemMapper(this.PathologyRequestItem, TempPathologyRequestItem);
     
      InitializeLayout();
      
      Grid MainGrid = GenerateMainGrid();

      GeneratePropteryValues(MainGrid);

      var ButtonStack = GenerateButtonStackPanel();   
      MainGrid.Children.Add(ButtonStack);

      //Border MainContentBoarder = new Border();
      //MainContentBoarder.BorderBrush = Brushes.CadetBlue;
      //MainContentBoarder.BorderThickness = new Thickness(3);
      //MainContentBoarder.Child = MainGrid;
      //DockPanel.SetDock(MainContentBoarder, System.Windows.Controls.Dock.Top);
      
      //SetContent(MainContentBoarder);
      SetContent(MainGrid);
    }
    
    private Button SaveButton;

    public void InitializeLayout()
    {
      Title = "Pathology Report";
      Width = 800;
      SizeToContent = SizeToContent.Height;
      ResizeMode = ResizeMode.NoResize;
      WindowStyle = WindowStyle.ToolWindow;
      DataContext = PathologyRequestItem;
      WindowStartupLocation = WindowStartupLocation.CenterOwner;      
    }
    
    private void GeneratePropteryValues(Grid MainGrid)
    {
      int LabelWidth = 110;
      
      
      var ReportId = GlobalStyleManager.GetValueParameterDockPanel("Report Id", LabelWidth, "ReportIdentifier");
      Grid.SetRow(ReportId, 0);
      Grid.SetColumn(ReportId, 0);
      Grid.SetColumnSpan(ReportId, 5);
      MainGrid.Children.Add(ReportId);

      var LocalCode = GlobalStyleManager.GetValueParameterDockPanel("Local Code", LabelWidth, "LocalCode");
      Grid.SetRow(LocalCode, 1);
      Grid.SetColumn(LocalCode, 0);
      Grid.SetColumnSpan(LocalCode, 4);
      MainGrid.Children.Add(LocalCode);

      var LocalDesc = GlobalStyleManager.GetValueParameterDockPanel("Desc", LabelWidth - 41, "LocalDescription");
      Grid.SetRow(LocalDesc, 1);
      Grid.SetColumn(LocalDesc, 4);
      Grid.SetColumnSpan(LocalDesc, 8);
      MainGrid.Children.Add(LocalDesc);

      var LocalSystemCode = GlobalStyleManager.GetValueParameterDockPanel("Local System", LabelWidth, "LocalSystemCode");
      Grid.SetRow(LocalSystemCode, 1);
      Grid.SetColumn(LocalSystemCode, 12);
      Grid.SetColumnSpan(LocalSystemCode, 4);
      MainGrid.Children.Add(LocalSystemCode);

      var SnomedCode = GlobalStyleManager.GetValueParameterDockPanel("Snomed Code", LabelWidth, "SnomedCode");
      Grid.SetRow(SnomedCode, 2);
      Grid.SetColumn(SnomedCode, 0);
      Grid.SetColumnSpan(SnomedCode, 4);
      MainGrid.Children.Add(SnomedCode);

      var SnomedPreferredTerm = GlobalStyleManager.GetValueParameterDockPanel("Pref Term", LabelWidth - 41, "SnomedPreferredTerm");
      Grid.SetRow(SnomedPreferredTerm, 2);
      Grid.SetColumn(SnomedPreferredTerm, 4);
      Grid.SetColumnSpan(SnomedPreferredTerm, 8);
      MainGrid.Children.Add(SnomedPreferredTerm);

      //var ReportedDateTime = new DateTimeGrid(LabelWidth);
      //ReportedDateTime.SetBinding(DateTimeGrid.DataContextProperty, "ReportedDateTime");      
      //Grid.SetRow(ReportedDateTime, 3);
      //Grid.SetColumn(ReportedDateTime, 0);
      //Grid.SetColumnSpan(ReportedDateTime, 12);
      //MainGrid.Children.Add(ReportedDateTime);

      DockPanel ReportedDateTimeFormatted = GlobalStyleManager.GetValueParameterDockPanel("Reported", LabelWidth, "ReportedDateTime.DateTimeFormatted", true);
      Grid.SetRow(ReportedDateTimeFormatted, 3);
      Grid.SetColumn(ReportedDateTimeFormatted, 0);
      Grid.SetColumnSpan(ReportedDateTimeFormatted, 6);
      MainGrid.Children.Add(ReportedDateTimeFormatted);

      Button EditReportedDateTimeButton = new Button();
      EditReportedDateTimeButton.Content = "Edit";
      EditReportedDateTimeButton.Margin = new Thickness(3);
      EditReportedDateTimeButton.Padding = new Thickness(3);
      Grid.SetRow(EditReportedDateTimeButton, 3);
      Grid.SetColumn(EditReportedDateTimeButton, 6);
      Grid.SetColumnSpan(EditReportedDateTimeButton, 1);      
      MainGrid.Children.Add(EditReportedDateTimeButton);
      EditReportedDateTimeButton.Click += new RoutedEventHandler((obj, e) =>
      {
        DateTimeWindow RequestedDateTimeWindow = new DateTimeWindow(PathologyRequestItem.ReportedDateTime);
        RequestedDateTimeWindow.Title = "Edit Reported Date & Time";
        RequestedDateTimeWindow.IsTimeOptional = true;
        RequestedDateTimeWindow.Owner = Window.GetWindow(this);
        RequestedDateTimeWindow.ShowDialog();
      });
      
      var DepartmentCode = GlobalStyleManager.GetValueParameterDockPanel("Department Code", LabelWidth, "DepartmentCode");
      Grid.SetRow(DepartmentCode, 3);
      Grid.SetColumn(DepartmentCode, 8);
      Grid.SetColumnSpan(DepartmentCode, 4);
      MainGrid.Children.Add(DepartmentCode);

      var ReportStatus = GlobalStyleManager.GetValueComboBoxEnumDockPanel("Report Status", LabelWidth, "ReportStatus", PathologyRequestItem.ReportStatusEnumDictionary.Select(x => x.Key).ToList());
      Grid.SetRow(ReportStatus, 3);
      Grid.SetColumn(ReportStatus, 12);
      Grid.SetColumnSpan(ReportStatus, 4);
      MainGrid.Children.Add(ReportStatus);

    }

    protected override void OnClosing(CancelEventArgs e)
    {
      if (this.DialogResult == null || this.DialogResult == false)
      {
        //Copy the unchanged values back as user has cancelled edit.
        PathologyRequestItemMapper(TempPathologyRequestItem, this.PathologyRequestItem);
        DialogResult = false;
      }

      base.OnClosing(e);
    }

    private StackPanel GenerateButtonStackPanel()
    {
      SaveButton = new Button();
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
      ButtonStack.Margin = new Thickness(3);

      Grid.SetRow(ButtonStack, 6);
      Grid.SetColumn(ButtonStack, 0);
      Grid.SetColumnSpan(ButtonStack, 16);

      return ButtonStack;
    }

    private void PathologyRequestItemMapper(PathologyRequestItemVM From, PathologyRequestItemVM To)
    {      
      To.LocalCode = From.LocalCode;
      To.LocalDescription = From.LocalDescription;
      To.LocalSystemCode = From.LocalSystemCode;
      To.SnomedCode = From.SnomedCode;
      To.SnomedPreferredTerm = From.SnomedPreferredTerm;

      To.ReportIdentifier = From.ReportIdentifier;
      To.ReportStatus= From.ReportStatus;
      To.DepartmentCode = From.DepartmentCode;
      To.ReportedDateTime = From.ReportedDateTime;      
    }

    private Grid GenerateMainGrid()
    {
      var Grid = GlobalStyleManager.GetGrid(6, 16);
      Grid.Margin = new Thickness(10);
      return Grid;      
    }
  }
}
