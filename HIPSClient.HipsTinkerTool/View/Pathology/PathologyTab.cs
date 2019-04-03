using HIPSClient.HipsTinkerTool.Controller;
using HIPSClient.HipsTinkerTool.Style;
using HIPSClient.HipsTinkerTool.View.Common;
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
      TabMainGrid.Children.Add(PatientGroup);

      Common.PatientGrid PatientGrid = new Common.PatientGrid(PathologyVM.Patient);
      PatientGroup.Content = PatientGrid;

      var PatientIdentiferGroup = new GroupBox();
      PatientIdentiferGroup.Header = "Patient Identifiers";
      Grid.SetRow(PatientIdentiferGroup, 0);      
      Grid.SetColumn(PatientIdentiferGroup, 1);      
      TabMainGrid.Children.Add(PatientIdentiferGroup);
     
      Common.PatientIdentifierGrid PatientIdentifierGrid = new Common.PatientIdentifierGrid(PathologyVM.PatientIdentifierList);
      PatientIdentiferGroup.Content = PatientIdentifierGrid;

      var PathologyOrderGroup = new GroupBox();
      PathologyOrderGroup.Header = "Order";
      Grid.SetRow(PathologyOrderGroup, 1);      
      Grid.SetColumn(PathologyOrderGroup, 0);      
      TabMainGrid.Children.Add(PathologyOrderGroup);

      Common.PathologyOrderGrid PathologyOrderGrid = new Common.PathologyOrderGrid(PathologyVM.Order);
      PathologyOrderGroup.Content = PathologyOrderGrid;

      var PatientRequestGroup = new GroupBox();
      PatientRequestGroup.Header = "Reports";
      Grid.SetRow(PatientRequestGroup, 1);      
      Grid.SetColumn(PatientRequestGroup, 1);      
      TabMainGrid.Children.Add(PatientRequestGroup);

      
      StackPanel PathologyRequestStack = new StackPanel();
      PathologyRequestStack.Orientation = Orientation.Vertical;
      PatientRequestGroup.Content = PathologyRequestStack;

      //Provider Name
      StackPanel AuthorStackPanel = new StackPanel();
      AuthorStackPanel.Orientation = Orientation.Horizontal;            
      AuthorStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
      PathologyRequestStack.Children.Add(AuthorStackPanel);

      DockPanel AuthorNameFormated = GlobalStyleManager.GetValueParameterDockPanel("Author", 50, "AuthorName.NameFormated", true);      
      AuthorStackPanel.Children.Add(AuthorNameFormated);

      Button EditAuthorNameButton = GlobalStyleManager.GetButton("Edit");      
      EditAuthorNameButton.Click += new RoutedEventHandler((obj, e) =>
      {
        var AuthorNameEditWindow = new NameWindow(PathologyVM.AuthorName);
        AuthorNameEditWindow.Title = "Edit Author Name";
        AuthorNameEditWindow.Owner = Window.GetWindow(this);
        AuthorNameEditWindow.ShowDialog();
      });
      AuthorStackPanel.Children.Add(EditAuthorNameButton);
      
      Common.PathologyRequestGrid PathologyRequestGrid = new Common.PathologyRequestGrid(PathologyVM.PathologyRequestList);
      PathologyRequestStack.Children.Add(PathologyRequestGrid);

      

    }

    private Grid GenerateMainGrid()
    {  
      return GlobalStyleManager.GetGrid(3, 2);
    }
  }
}
