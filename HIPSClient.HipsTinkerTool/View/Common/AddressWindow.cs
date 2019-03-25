using HIPSClient.HipsTinkerTool.Style;
using HIPSClient.HipsTinkerTool.ViewModel.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public class AddressWindow : BaseValidationWindow, IView
  {
    public AddressVM AddressVM { get; private set; }
    public AddressVM TempAddressVM { get; private set; }

    public AddressWindow(AddressVM AddressVM)
      : base(AddressVM)
    {
      this.AddressVM = AddressVM;
      DataContext = this.AddressVM;

      TempAddressVM = new AddressVM();
      AddressVMMapper(AddressVM, TempAddressVM);

      InitializeLayout();
      Grid MainGrid = GenerateControls();
      StackPanel ButtonStack = GenerateButtonStackPanel();
      Grid.SetColumn(ButtonStack, 0);
      Grid.SetRow(ButtonStack, 8);
      MainGrid.Children.Add(ButtonStack);

      Border MainContentBoarder = new Border();
      MainContentBoarder.Background = Brushes.GhostWhite;
      MainContentBoarder.BorderBrush = Brushes.Silver;
      MainContentBoarder.BorderThickness = new Thickness(3);
      MainContentBoarder.Child = MainGrid;
      MainContentBoarder.HorizontalAlignment = HorizontalAlignment.Stretch;
      this.SetContent(MainContentBoarder);
    }

    private void AddressVMMapper(AddressVM From, AddressVM To)
    {
      To.AddressLineOne = From.AddressLineOne;
      To.AddressLineTwo = From.AddressLineTwo;
      To.Suburb = From.Suburb;
      To.PostCode = From.PostCode;
      To.State = From.State;
      To.Country = From.Country;
    }

    public void InitializeLayout()
    {
      Title = "Address";
      Width = 500;
      Height = 300;
      ResizeMode = ResizeMode.NoResize;
      WindowStyle = WindowStyle.None;
      WindowStartupLocation = WindowStartupLocation.CenterOwner;

    }

    private Grid GenerateControls()
    {
      int LabelWidth = 80;
      Grid MainGrid = GenerateMainGrid();
      DockPanel AddressLineOne = GlobalStyleManager.GetValueParameterDockPanel("Line One", LabelWidth, "AddressLineOne");
      Grid.SetColumn(AddressLineOne, 0);
      Grid.SetColumnSpan(AddressLineOne, 3);
      Grid.SetRow(AddressLineOne, 2);
      MainGrid.Children.Add(AddressLineOne);

      DockPanel AddressLineTwo = GlobalStyleManager.GetValueParameterDockPanel("Line Two", LabelWidth, "AddressLineTwo");
      Grid.SetColumn(AddressLineTwo, 0);
      Grid.SetColumnSpan(AddressLineTwo, 3);
      Grid.SetRow(AddressLineTwo, 3);
      MainGrid.Children.Add(AddressLineTwo);

      DockPanel Suburb = GlobalStyleManager.GetValueParameterDockPanel("Suburb", LabelWidth, "Suburb");
      Grid.SetColumn(Suburb, 0);
      Grid.SetColumnSpan(Suburb, 3);
      Grid.SetRow(Suburb, 4);
      MainGrid.Children.Add(Suburb);

      DockPanel PostCode = GlobalStyleManager.GetValueParameterDockPanel("Postcode", LabelWidth, "PostCode");
      Grid.SetColumn(PostCode, 0);
      Grid.SetColumnSpan(PostCode, 3);
      Grid.SetRow(PostCode, 5);
      MainGrid.Children.Add(PostCode);

      DockPanel State = GlobalStyleManager.GetValueParameterDockPanel("State", LabelWidth, "State");
      Grid.SetColumn(State, 0);
      Grid.SetColumnSpan(State, 3);
      Grid.SetRow(State, 6);
      MainGrid.Children.Add(State);

      DockPanel Country = GlobalStyleManager.GetValueParameterDockPanel("Country", LabelWidth, "Country");
      Grid.SetColumn(Country, 0);
      Grid.SetColumnSpan(Country, 3);
      Grid.SetRow(Country, 7);
      MainGrid.Children.Add(Country);

      return MainGrid;
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
        AddressVMMapper(TempAddressVM, AddressVM);        
        DialogResult = false;
        this.Close();
      });

      StackPanel ButtonStack = new StackPanel();
      ButtonStack.Orientation = Orientation.Horizontal;
      ButtonStack.Children.Add(CancelPatientIdButton);
      ButtonStack.Children.Add(SavePatientIdButton);
      ButtonStack.HorizontalAlignment = HorizontalAlignment.Right;

      Grid.SetRow(ButtonStack, 4);
      Grid.SetColumn(ButtonStack, 0);

      return ButtonStack;
    }

    private Grid GenerateMainGrid()
    {
      var Col0 = new ColumnDefinition() { MinWidth = 400 };

      var Row0 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row1 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row2 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row3 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row4 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row5 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row6 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row7 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row8 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };

      var Grid = new Grid();

      Grid.ColumnDefinitions.Add(Col0);
      Grid.HorizontalAlignment = HorizontalAlignment.Stretch;

      Grid.RowDefinitions.Add(Row0);
      Grid.RowDefinitions.Add(Row1);
      Grid.RowDefinitions.Add(Row2);
      Grid.RowDefinitions.Add(Row3);
      Grid.RowDefinitions.Add(Row4);
      Grid.RowDefinitions.Add(Row5);
      Grid.RowDefinitions.Add(Row6);
      Grid.RowDefinitions.Add(Row7);
      Grid.RowDefinitions.Add(Row8);

      return Grid;
    }
  }
}
