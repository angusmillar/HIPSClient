using HIPSClient.Common.Tools.Enum;
using HIPSClient.Hips.Model;
using HIPSClient.HipsTinkerTool.Controller;
using HIPSClient.HipsTinkerTool.Style;
using HIPSClient.HipsTinkerTool.ViewModel.Common;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public class PathologyReportListGrid : Grid, IView
  {
    public ObservableCollection<PathologyReportItemVM> PathologyReportList { get; private set; }
    
    private ListBox PathologyReportListView;

    public PathologyReportListGrid(ObservableCollection<PathologyReportItemVM> PathologyRequestList)
    {
      this.PathologyReportList = PathologyRequestList;
      InitializeLayout();
    }

    public void InitializeLayout()
    {
      GenerateMainGrid();

      PathologyReportListView = GeneratePathologyReportListBox();
      Grid.SetRow(PathologyReportListView, 0);
      Grid.SetRowSpan(PathologyReportListView, 3);
      Grid.SetColumn(PathologyReportListView, 0);
      Grid.SetColumnSpan(PathologyReportListView, 8);
      this.Children.Add(PathologyReportListView);

      StackPanel ButtonStack = GenerateButtonStackPanel();
      ButtonStack.HorizontalAlignment = HorizontalAlignment.Right;
      Grid.SetRow(ButtonStack, 3);
      Grid.SetColumn(ButtonStack, 0);
      Grid.SetColumnSpan(ButtonStack, 8);
      this.Children.Add(ButtonStack);
      
    }
    
    private StackPanel GenerateButtonStackPanel()
    {
      Button RemoveButton = GlobalStyleManager.GetButton("Remove");
      RemoveButton.HorizontalAlignment = HorizontalAlignment.Right;      
      RemoveButton.Click += new RoutedEventHandler((obj, e) =>
      {
        if (PathologyReportListView.SelectedItem is PathologyReportItemVM Item)
        {
          this.PathologyReportList.Remove(Item);          
        }
      });


      Button EditButton = GlobalStyleManager.GetButton("Edit");      
      EditButton.HorizontalAlignment = HorizontalAlignment.Right;      
      EditButton.Click += new RoutedEventHandler((obj, e) =>
      {
        if (PathologyReportListView.SelectedItem is PathologyReportItemVM Item)
        {
          var EditPathReportWindow = new PathologyReportEditWindow(Item);
          EditPathReportWindow.Title = "Edit Pathology Report";
          EditPathReportWindow.Owner = Window.GetWindow(this);
          EditPathReportWindow.ShowDialog();
        }
      });

      Button AddButton = GlobalStyleManager.GetButton("Add");      
      AddButton.HorizontalAlignment = HorizontalAlignment.Right;     
      AddButton.Click += new RoutedEventHandler((obj, e) =>
      {
        var NewReportItem = new PathologyReportItemVM();
        NewReportItem.ReportIdentifier = string.Empty;
        NewReportItem.LocalCode = string.Empty;
        NewReportItem.LocalDescription = string.Empty;
        NewReportItem.LocalSystemCode = string.Empty;
        NewReportItem.SnomedCode = string.Empty;
        NewReportItem.SnomedPreferredTerm = string.Empty;
        NewReportItem.ReportedDateTime = DateTimeVM.Now();        
        NewReportItem.ReportStatus = ResultStatus.Final.GetUIDisplay();        
        var EditPathReportWindow = new PathologyReportEditWindow(NewReportItem);
        EditPathReportWindow.Title = "Add Pathology Report";
        EditPathReportWindow.Owner = Window.GetWindow(this);
        if (EditPathReportWindow.ShowDialog().Value)
        {
          this.PathologyReportList.Add(NewReportItem);          
        }
      });
    
      StackPanel ButtonStack = new StackPanel();
      ButtonStack.Orientation = Orientation.Horizontal;
      ButtonStack.Children.Add(AddButton);
      ButtonStack.Children.Add(EditButton);
      ButtonStack.Children.Add(RemoveButton);
      ButtonStack.HorizontalAlignment = HorizontalAlignment.Right;

      return ButtonStack;
    }

    private ListBox GeneratePathologyReportListBox()
    {
      var ReportIdTextBoxFactory = new FrameworkElementFactory(typeof(Label));
      ReportIdTextBoxFactory.SetValue(Label.WidthProperty, 100.0);
      ReportIdTextBoxFactory.SetValue(Label.FontWeightProperty, FontWeights.DemiBold);      
      ReportIdTextBoxFactory.SetBinding(Label.ContentProperty, new Binding("ReportIdentifier"));

      var LocalDescTextBoxFactory = new FrameworkElementFactory(typeof(Label));      
      LocalDescTextBoxFactory.SetValue(Label.WidthProperty, 120.0);
      LocalDescTextBoxFactory.SetBinding(Label.ContentProperty, new Binding("LocalDescription"));

      var StatusTextBoxFactory = new FrameworkElementFactory(typeof(Label));
      StatusTextBoxFactory.SetValue(Label.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
      StatusTextBoxFactory.SetValue(Label.WidthProperty, 70.0);
      StatusTextBoxFactory.SetBinding(Label.ContentProperty, new Binding("ReportStatus"));


      var ReportedDateTextBoxFactory = new FrameworkElementFactory(typeof(Label));
      ReportedDateTextBoxFactory.SetValue(Label.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
      ReportedDateTextBoxFactory.SetBinding(Label.ContentProperty, new Binding("ReportedDateTime.DateTimeFormatted"));


      var StackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
      StackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
      StackPanelFactory.SetValue(StackPanel.HorizontalAlignmentProperty, HorizontalAlignment.Center);

      StackPanelFactory.AppendChild(ReportIdTextBoxFactory);
      StackPanelFactory.AppendChild(LocalDescTextBoxFactory);
      StackPanelFactory.AppendChild(StatusTextBoxFactory);
      StackPanelFactory.AppendChild(ReportedDateTextBoxFactory);

      DataTemplate Template = new DataTemplate();
      Template.VisualTree = StackPanelFactory;

      ListBox ResultListBox = new ListBox();
      ResultListBox.HorizontalAlignment = HorizontalAlignment.Stretch;
      ResultListBox.Height = 90;
      ResultListBox.Margin = new Thickness(3);
      ResultListBox.SelectionMode = SelectionMode.Single;
      ResultListBox.Name = "ListBoxPathologyRequests";
      ResultListBox.SelectedIndex = 0;
      BindingOperations.SetBinding(ResultListBox, ListView.ItemsSourceProperty, new Binding("PathologyRequestList"));
      ResultListBox.ItemTemplate = Template;
      return ResultListBox;
    }

    private void GenerateMainGrid()
    {
      this.SetGrid(5, 8);     
    }
  }
}
