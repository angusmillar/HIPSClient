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

      var PatientIdentiferGroup = new GroupBox();
      PatientIdentiferGroup.Header = "Patient Identifiers";
      Grid.SetRow(PatientIdentiferGroup, 0);
      Grid.SetColumn(PatientIdentiferGroup, 0);
      Grid.SetColumnSpan(PatientIdentiferGroup, 2);
      TabMainGrid.Children.Add(PatientIdentiferGroup);
     
      Common.PatientIdentifierGrid PatientIdentifierGrid = new Common.PatientIdentifierGrid(PathologyVM.PatientIdentifierList);
      PatientIdentiferGroup.Content = PatientIdentifierGrid;

      
    }
    
    private Grid GenerateMainGrid()
    {
      var Col1 = new ColumnDefinition();
      var Col2 = new ColumnDefinition();
      var Col3 = new ColumnDefinition();
      var Col4 = new ColumnDefinition();

      var Row1 = new RowDefinition();
      var Row2 = new RowDefinition(); 
      var Row3 = new RowDefinition();
      var Row4 = new RowDefinition();

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
