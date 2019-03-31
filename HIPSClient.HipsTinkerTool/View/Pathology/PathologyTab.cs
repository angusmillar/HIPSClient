using HIPSClient.HipsTinkerTool.Controller;
using HIPSClient.HipsTinkerTool.ViewModel.Pathology;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HIPSClient.HipsTinkerTool.View.Pathology
{
  public class PathologyTab : TabItem, IView
  {
    public PathologyVM PathologyVM { get; private set; }
    public PathologyController Controller { get; private set; }

    public PathologyTab()
    {
      PathologyVM = new PathologyVM();
      Controller = new PathologyController(this);
      InitializeLayout();
    }

    public void InitializeLayout()
    {
      Header = "Pathology";
      Grid TabMainGrid = GenerateMainGrid();      

      //The Main Tabs Content
      Content = TabMainGrid;
      
      //The ViewModel
      DataContext = PathologyVM;

      var PatientGroup = new GroupBox();
      PatientGroup.Header = "Patient";
      Grid.SetRow(PatientGroup, 0);
      Grid.SetColumn(PatientGroup, 0);
      Grid.SetColumnSpan(PatientGroup, 2);
      TabMainGrid.Children.Add(PatientGroup);

      Common.PatientGrid PatientGrid = new Common.PatientGrid(PathologyVM.Patient);
      PatientGroup.Content = PatientGrid;

      var PatientIdentiferGroup = new GroupBox();
      PatientIdentiferGroup.Header = "Patient Identifiers";
      Grid.SetRow(PatientIdentiferGroup, 0);
      Grid.SetRowSpan(PatientIdentiferGroup, 3);
      Grid.SetColumn(PatientIdentiferGroup, 2);
      Grid.SetColumnSpan(PatientIdentiferGroup, 2);
      TabMainGrid.Children.Add(PatientIdentiferGroup);
     
      Common.PatientIdentifierGrid PatientIdentifierGrid = new Common.PatientIdentifierGrid(PathologyVM.PatientIdentifierList);
      PatientIdentiferGroup.Content = PatientIdentifierGrid;

      var PathologyOrderGroup = new GroupBox();
      PathologyOrderGroup.Header = "Order";
      Grid.SetRow(PathologyOrderGroup, 5);
      Grid.SetRowSpan(PathologyOrderGroup, 2);
      Grid.SetColumn(PathologyOrderGroup, 0);
      Grid.SetColumnSpan(PathologyOrderGroup, 2);
      TabMainGrid.Children.Add(PathologyOrderGroup);

      Common.PathologyOrderGrid PathologyOrderGrid = new Common.PathologyOrderGrid(PathologyVM.Order);
      PathologyOrderGroup.Content = PathologyOrderGrid;

      var PatientRequestGroup = new GroupBox();
      PatientRequestGroup.Header = "Requests";
      Grid.SetRow(PatientRequestGroup, 5);
      Grid.SetRowSpan(PatientRequestGroup, 2);
      Grid.SetColumn(PatientRequestGroup, 2);
      Grid.SetColumnSpan(PatientRequestGroup, 2);
      TabMainGrid.Children.Add(PatientRequestGroup);

      Common.PathologyRequestGrid PathologyRequestGrid = new Common.PathologyRequestGrid(PathologyVM.PathologyRequestList);
      PatientRequestGroup.Content = PathologyRequestGrid;

    }

    private Grid GenerateMainGrid()
    {
      var Col1 = new ColumnDefinition();
      var Col2 = new ColumnDefinition();
      var Col3 = new ColumnDefinition();
      var Col4 = new ColumnDefinition();

      var Row1 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row2 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row3 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row4 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };

      var Grid = new Grid();

      Grid.ColumnDefinitions.Add(Col1);
      Grid.ColumnDefinitions.Add(Col2);
      Grid.ColumnDefinitions.Add(Col3);
      Grid.ColumnDefinitions.Add(Col4);

      Grid.RowDefinitions.Add(Row1);
      Grid.RowDefinitions.Add(Row2);
      Grid.RowDefinitions.Add(Row3);
      Grid.RowDefinitions.Add(Row4);

      return Grid;
    }
  }
}
