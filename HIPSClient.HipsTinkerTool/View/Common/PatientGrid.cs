using HIPSClient.HipsTinkerTool.Controller;
using HIPSClient.HipsTinkerTool.Style;
using HIPSClient.HipsTinkerTool.ViewModel.Common;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public class PatientGrid : Grid, IView
  {
    public PatientVM PatientVM { get; private set; }
    public PatientController Controller { get; private set; }


    public PatientGrid(PatientVM PatientVM)
    {
      this.PatientVM = PatientVM;
      this.Controller = new PatientController(this);
      InitializeLayout();
      GenerateControls();
    }

    private void GenerateControls()
    {
      int ColOneLabelWidth = 60;
      int ColTwoLabelWidth = 40;
      int ColThreeLabelWidth = 40;
      
      DockPanel Family = GlobalStyleManager.GetValueParameterDockPanel("Family", ColOneLabelWidth, "Patient.FamilyName");
      Grid.SetColumn(Family, 0);
      Grid.SetColumnSpan(Family, 3);
      Grid.SetRow(Family, 0);
      this.Children.Add(Family);

      DockPanel Given = GlobalStyleManager.GetValueParameterDockPanel("Given", ColTwoLabelWidth, "Patient.GivenName");
      Grid.SetColumn(Given, 3);
      Grid.SetColumnSpan(Given, 3);
      Grid.SetRow(Given, 0);
      this.Children.Add(Given);

      DockPanel Title = GlobalStyleManager.GetValueParameterDockPanel("Title", ColThreeLabelWidth, "Patient.Title");
      Grid.SetColumn(Title, 6);
      Grid.SetColumnSpan(Title, 2);
      Grid.SetRow(Title, 0);
      this.Children.Add(Title);

      DockPanel Gender = GlobalStyleManager.GetValueComboBoxEnumDockPanel("Sex", ColOneLabelWidth, "Patient.Gender", PatientVM.GenderEnumDictionary.Select(x => x.Key).ToList());
      Grid.SetColumn(Gender, 0);
      Grid.SetColumnSpan(Gender, 3);
      Grid.SetRow(Gender, 1);
      this.Children.Add(Gender);

      DockPanel DOB = GlobalStyleManager.GetValueDatePickerDockPanel("DOB", ColTwoLabelWidth, "Patient.DateOfBirth");
      Grid.SetColumn(DOB, 3);
      Grid.SetColumnSpan(DOB, 3);
      Grid.SetRow(DOB, 1);
      this.Children.Add(DOB);

      DockPanel AddressFormated = GlobalStyleManager.GetValueParameterDockPanel("Address", ColOneLabelWidth, "Patient.Address.AddressFormated", true);
      Grid.SetColumn(AddressFormated, 0);
      Grid.SetColumnSpan(AddressFormated, 6);
      Grid.SetRow(AddressFormated, 3);
      this.Children.Add(AddressFormated);

      Button EditAddressButton = new Button();
      EditAddressButton.Content = "Edit Address";
      EditAddressButton.Margin = new Thickness(3);
      EditAddressButton.Padding = new Thickness(3);      
      Grid.SetColumn(EditAddressButton, 6);
      Grid.SetColumnSpan(EditAddressButton, 3);
      Grid.SetRow(EditAddressButton, 3);
      this.Children.Add(EditAddressButton);
      EditAddressButton.Click += new RoutedEventHandler((obj, e) =>
      {
        var AddressEditWindow = new AddressWindow(PatientVM.Address);
        AddressEditWindow.Owner = Window.GetWindow(this);
        AddressEditWindow.ShowDialog();        
      });

      DockPanel HomePhone = GlobalStyleManager.GetValueParameterDockPanel("Home Ph", ColOneLabelWidth, "Patient.HomePhone");
      Grid.SetColumn(HomePhone, 0);
      Grid.SetColumnSpan(HomePhone, 3);
      Grid.SetRow(HomePhone, 4);
      this.Children.Add(HomePhone);

      DockPanel WorkPhone = GlobalStyleManager.GetValueParameterDockPanel("Work Ph", ColOneLabelWidth, "Patient.WorkPhone");
      Grid.SetColumn(WorkPhone, 3);
      Grid.SetColumnSpan(WorkPhone, 3);
      Grid.SetRow(WorkPhone, 4);
      this.Children.Add(WorkPhone);



    }

    //private RoutedEventHandler RoutedEventHandler(Func<object, object, object> p)
    //{
    //  throw new NotImplementedException();
    //}

    public void InitializeLayout()
    {
      GenerateMainGrid();
    }


    private void GenerateMainGrid()
    {
      this.Margin = new Thickness(5);
      var Col0 = new ColumnDefinition();
      var Col1 = new ColumnDefinition();
      var Col2 = new ColumnDefinition();
      var Col3 = new ColumnDefinition();
      var Col4 = new ColumnDefinition();
      var Col5 = new ColumnDefinition();
      var Col6 = new ColumnDefinition();
      var Col7 = new ColumnDefinition();

      var Row0 = new RowDefinition() { Height = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) };
      var Row1 = new RowDefinition() { Height = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) };
      var Row2 = new RowDefinition() { Height = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) };
      var Row3 = new RowDefinition() { Height = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) };
      var Row4 = new RowDefinition() { Height = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) };
      var Row5 = new RowDefinition() { Height = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) };
      var Row6 = new RowDefinition() { Height = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) };
      var Row7 = new RowDefinition() { Height = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) };

      this.ColumnDefinitions.Add(Col0);
      this.ColumnDefinitions.Add(Col1);
      this.ColumnDefinitions.Add(Col2);
      this.ColumnDefinitions.Add(Col3);
      this.ColumnDefinitions.Add(Col4);
      this.ColumnDefinitions.Add(Col5);
      this.ColumnDefinitions.Add(Col6);
      this.ColumnDefinitions.Add(Col7);

      this.RowDefinitions.Add(Row0);
      this.RowDefinitions.Add(Row1);
      this.RowDefinitions.Add(Row2);
      this.RowDefinitions.Add(Row3);
      this.RowDefinitions.Add(Row4);
      this.RowDefinitions.Add(Row5);
      this.RowDefinitions.Add(Row6);
      this.RowDefinitions.Add(Row7);

    }
  }
}
