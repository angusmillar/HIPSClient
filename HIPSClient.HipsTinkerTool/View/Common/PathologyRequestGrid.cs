using HIPSClient.Common.Tools.Enum;
using HIPSClient.HipsTinkerTool.Controller;
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

      PathologyRequestListView = GeneratePatientIdentifierListBox();
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
      Button RemovePatientIdButton = new Button();
      RemovePatientIdButton.Content = "Remove";
      RemovePatientIdButton.Width = 60;
      RemovePatientIdButton.HorizontalAlignment = HorizontalAlignment.Right;
      RemovePatientIdButton.Margin = new Thickness(3);
      RemovePatientIdButton.Padding = new Thickness(3);
      RemovePatientIdButton.Click += new RoutedEventHandler((obj, e) =>
      {
        if (PathologyRequestListView.SelectedItem is PathologyRequestItemVM Item)
        {
          this.PathologyRequestList.Remove(Item);          
        }
      });


      Button EditPatientIdButton = new Button();
      EditPatientIdButton.Content = "Edit";
      EditPatientIdButton.Width = 60;
      EditPatientIdButton.HorizontalAlignment = HorizontalAlignment.Right;
      EditPatientIdButton.Margin = new Thickness(3);
      EditPatientIdButton.Padding = new Thickness(3);
      EditPatientIdButton.Click += new RoutedEventHandler((obj, e) =>
      {
        if (PathologyRequestListView.SelectedItem is PathologyRequestItemVM Item)
        {
          var EditPathRequestWindow = new PathologyRequestEditWindow(Item);
          EditPathRequestWindow.Title = "Edit Pathology Request";
          EditPathRequestWindow.Owner = Window.GetWindow(this);
          EditPathRequestWindow.ShowDialog();
        }
      });

      Button AddPatientIdButon = new Button();
      AddPatientIdButon.Content = "Add";
      AddPatientIdButon.Width = 60;
      AddPatientIdButon.HorizontalAlignment = HorizontalAlignment.Right;
      AddPatientIdButon.Margin = new Thickness(3);
      AddPatientIdButon.Padding = new Thickness(3);
      AddPatientIdButon.Click += new RoutedEventHandler((obj, e) =>
      {
        var NewRequestItem = new PathologyRequestItemVM();
        NewRequestItem.Type = HIPSClient.Hips.Model.PatientIdentifierType.MedicalRecordNumber.GetUIDisplay();
        NewRequestItem.AssigningAuthority = string.Empty;
        NewRequestItem.IdentifierType = HIPSClient.Hips.Model.PatientIdentifierType.MedicalRecordNumber.GetLiteral();
        NewRequestItem.Value = string.Empty;
        var EditPathRequestWindow = new PathologyRequestEditWindow(NewRequestItem);
        EditPathRequestWindow.Owner = Window.GetWindow(this);
        if (EditPathRequestWindow.ShowDialog().Value)
        {
          this.PathologyRequestList.Add(NewRequestItem);          
        }
      });
    
      StackPanel ButtonStack = new StackPanel();
      ButtonStack.Orientation = Orientation.Horizontal;
      ButtonStack.Children.Add(AddPatientIdButon);
      ButtonStack.Children.Add(EditPatientIdButton);
      ButtonStack.Children.Add(RemovePatientIdButton);
      ButtonStack.HorizontalAlignment = HorizontalAlignment.Right;

      return ButtonStack;
    }

    private ListBox GeneratePatientIdentifierListBox()
    {
      var TypeTextBoxFactory = new FrameworkElementFactory(typeof(Label));
      TypeTextBoxFactory.SetValue(Label.WidthProperty, 143.0);
      TypeTextBoxFactory.SetValue(Label.FontWeightProperty, FontWeights.DemiBold);
      TypeTextBoxFactory.SetBinding(Label.ContentProperty, new Binding("Type"));

      var AssignAuthBoxFactory = new FrameworkElementFactory(typeof(Label));
      AssignAuthBoxFactory.SetValue(Label.WidthProperty, 100.0);      
      AssignAuthBoxFactory.SetBinding(Label.ContentProperty, new Binding("AssigningAuthority"));

      var ValueTextBoxFactory = new FrameworkElementFactory(typeof(Label));
      ValueTextBoxFactory.SetValue(Label.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
      ValueTextBoxFactory.SetBinding(Label.ContentProperty, new Binding("Value"));

      var StackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
      StackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
      StackPanelFactory.SetValue(StackPanel.HorizontalAlignmentProperty, HorizontalAlignment.Center);

      StackPanelFactory.AppendChild(TypeTextBoxFactory);
      StackPanelFactory.AppendChild(AssignAuthBoxFactory);
      StackPanelFactory.AppendChild(ValueTextBoxFactory);

      DataTemplate Template = new DataTemplate();
      Template.VisualTree = StackPanelFactory;

      ListBox ResultListBox = new ListBox();
      ResultListBox.HorizontalAlignment = HorizontalAlignment.Stretch;
      ResultListBox.Height = 90;
      ResultListBox.Margin = new Thickness(3);
      ResultListBox.SelectionMode = SelectionMode.Single;
      ResultListBox.Name = "ListBoxPatientIdList";
      ResultListBox.SelectedIndex = 0;
      BindingOperations.SetBinding(ResultListBox, ListView.ItemsSourceProperty, new Binding("PatientIdentifierList"));
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

      var Grid = new Grid();

      this.ColumnDefinitions.Add(Col1);


      this.RowDefinitions.Add(Row1);
      this.RowDefinitions.Add(Row2);
      this.RowDefinitions.Add(Row3);
      this.RowDefinitions.Add(Row4);

    }
  }
}
