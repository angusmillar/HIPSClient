using HIPSClient.Common.Tools.Enum;
using HIPSClient.Hips.Model;
using HIPSClient.HipsTinkerTool.Controller;
using HIPSClient.HipsTinkerTool.Style;
using HIPSClient.HipsTinkerTool.ViewModel.Common;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public class PathologyRequestGrid : Grid, IView
  {
    public ObservableCollection<PathologyRequestItemVM> PathologyRequestList { get; private set; }
    
    private ListBox PathologyRequestListView;

    public PathologyRequestGrid(ObservableCollection<PathologyRequestItemVM> PathologyRequestList)
    {
      this.PathologyRequestList = PathologyRequestList;
      InitializeLayout();
    }

    public void InitializeLayout()
    {
      GenerateMainGrid();

      PathologyRequestListView = GeneratePathologyRequestListBox();
      Grid.SetRow(PathologyRequestListView, 0);
      Grid.SetRowSpan(PathologyRequestListView, 3);
      Grid.SetColumn(PathologyRequestListView, 0);

      this.Children.Add(PathologyRequestListView);

      StackPanel ButtonStack = GenerateButtonStackPanel();
      Grid.SetRow(ButtonStack, 4);
      Grid.SetColumn(ButtonStack, 0);
      this.Children.Add(ButtonStack);

    }

    private StackPanel GenerateButtonStackPanel()
    {
      Button RemoveButton = GlobalStyleManager.GetButton("Remove");
      RemoveButton.HorizontalAlignment = HorizontalAlignment.Right;      
      RemoveButton.Click += new RoutedEventHandler((obj, e) =>
      {
        if (PathologyRequestListView.SelectedItem is PathologyRequestItemVM Item)
        {
          this.PathologyRequestList.Remove(Item);          
        }
      });


      Button EditButton = GlobalStyleManager.GetButton("Edit");      
      EditButton.HorizontalAlignment = HorizontalAlignment.Right;      
      EditButton.Click += new RoutedEventHandler((obj, e) =>
      {
        if (PathologyRequestListView.SelectedItem is PathologyRequestItemVM Item)
        {
          var EditPathRequestWindow = new PathologyRequestEditWindow(Item);
          EditPathRequestWindow.Title = "Edit Pathology Report";
          EditPathRequestWindow.Owner = Window.GetWindow(this);
          EditPathRequestWindow.ShowDialog();
        }
      });

      Button AddButton = GlobalStyleManager.GetButton("Add");      
      AddButton.HorizontalAlignment = HorizontalAlignment.Right;     
      AddButton.Click += new RoutedEventHandler((obj, e) =>
      {
        var NewRequestItem = new PathologyRequestItemVM();
        NewRequestItem.ReportIdentifier = string.Empty;
        NewRequestItem.LocalCode = string.Empty;
        NewRequestItem.LocalDescription = string.Empty;
        NewRequestItem.LocalSystemCode = string.Empty;
        NewRequestItem.SnomedCode = string.Empty;
        NewRequestItem.SnomedPreferredTerm = string.Empty;
        NewRequestItem.ReportedDateTime = DateTimeVM.Now();        
        NewRequestItem.ReportStatus = ResultStatus.Final.GetUIDisplay();        
        var EditPathRequestWindow = new PathologyRequestEditWindow(NewRequestItem);
        EditPathRequestWindow.Title = "Add Pathology Report";
        EditPathRequestWindow.Owner = Window.GetWindow(this);
        if (EditPathRequestWindow.ShowDialog().Value)
        {
          this.PathologyRequestList.Add(NewRequestItem);          
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

    private ListBox GeneratePathologyRequestListBox()
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
      this.Margin = new Thickness(5);
      var Col1 = new ColumnDefinition();

      var Row1 = new RowDefinition() { Height = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) };
      var Row2 = new RowDefinition() { Height = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) };
      var Row3 = new RowDefinition() { Height = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) };
      var Row4 = new RowDefinition() { Height = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) };
      
      this.ColumnDefinitions.Add(Col1);


      this.RowDefinitions.Add(Row1);
      this.RowDefinitions.Add(Row2);
      this.RowDefinitions.Add(Row3);
      this.RowDefinitions.Add(Row4);

    }
  }
}
