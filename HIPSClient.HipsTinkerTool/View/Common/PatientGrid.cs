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
      
      
      DockPanel PatientNameFormated = GlobalStyleManager.GetValueParameterDockPanel("Name", ColOneLabelWidth, "Patient.PatientName.NameFormated", true);
      Grid.SetColumn(PatientNameFormated, 0);
      Grid.SetColumnSpan(PatientNameFormated, 6);
      Grid.SetRow(PatientNameFormated, 0);
      this.Children.Add(PatientNameFormated);

      Button EditPatientNameButton = GlobalStyleManager.GetButton("Edit");
      EditPatientNameButton.HorizontalAlignment = HorizontalAlignment.Left;
      Grid.SetColumn(EditPatientNameButton, 6);
      Grid.SetColumnSpan(EditPatientNameButton, 2);
      Grid.SetRow(EditPatientNameButton, 0);
      this.Children.Add(EditPatientNameButton);
      EditPatientNameButton.Click += new RoutedEventHandler((obj, e) =>
      {
        var PatientNameEditWindow = new NameWindow(PatientVM.PatientName);
        PatientNameEditWindow.Title = "Edit Patient Name";
        PatientNameEditWindow.Owner = Window.GetWindow(this);
        PatientNameEditWindow.ShowDialog();
      });
            
      DockPanel Gender = GlobalStyleManager.GetValueComboBoxEnumDockPanel("Sex", ColOneLabelWidth, "Patient.GenderFormatted", PatientVM.GenderEnumDictionary.Select(x => x.Key).ToList());
      Grid.SetColumn(Gender, 0);
      Grid.SetColumnSpan(Gender, 3);
      Grid.SetRow(Gender, 1);
      this.Children.Add(Gender);

      DockPanel DOB = GlobalStyleManager.GetValueDatePickerDockPanel("DOB", ColTwoLabelWidth, "PatientDateOfBirth");
      Grid.SetColumn(DOB, 3);
      Grid.SetColumnSpan(DOB, 3);
      Grid.SetRow(DOB, 1);
      this.Children.Add(DOB);

      DockPanel AddressFormated = GlobalStyleManager.GetValueParameterDockPanel("Address", ColOneLabelWidth, "Patient.Address.AddressFormated", true);
      Grid.SetColumn(AddressFormated, 0);
      Grid.SetColumnSpan(AddressFormated, 6);
      Grid.SetRow(AddressFormated, 2);
      this.Children.Add(AddressFormated);

      Button EditAddressButton = GlobalStyleManager.GetButton("Edit");
      EditAddressButton.HorizontalAlignment = HorizontalAlignment.Left;
      Grid.SetColumn(EditAddressButton, 6);
      Grid.SetColumnSpan(EditAddressButton, 2);
      Grid.SetRow(EditAddressButton, 2);
      this.Children.Add(EditAddressButton);
      EditAddressButton.Click += new RoutedEventHandler((obj, e) =>
      {
        var AddressEditWindow = new AddressWindow(PatientVM.Address);
        AddressEditWindow.Title = "Edit Patient Address";
        AddressEditWindow.Owner = Window.GetWindow(this);
        AddressEditWindow.ShowDialog();        
      });

      DockPanel HomePhone = GlobalStyleManager.GetValueParameterDockPanel("Home Ph", ColOneLabelWidth, "Patient.HomePhone");
      Grid.SetColumn(HomePhone, 0);
      Grid.SetColumnSpan(HomePhone, 3);
      Grid.SetRow(HomePhone, 3);
      this.Children.Add(HomePhone);

      DockPanel WorkPhone = GlobalStyleManager.GetValueParameterDockPanel("Work Ph", ColOneLabelWidth, "Patient.WorkPhone");
      Grid.SetColumn(WorkPhone, 3);
      Grid.SetColumnSpan(WorkPhone, 3);
      Grid.SetRow(WorkPhone, 3);
      this.Children.Add(WorkPhone);

      DockPanel IndigenousStatus = GlobalStyleManager.GetValueComboBoxEnumDockPanel("Indigenous Status", 120, "Patient.IndigenousStatus", PatientVM.IndigenousStatusEnumDictionary.Select(x => x.Key).ToList());
      Grid.SetColumn(IndigenousStatus, 0);
      Grid.SetColumnSpan(IndigenousStatus, 8);
      Grid.SetRow(IndigenousStatus, 4);
      this.Children.Add(IndigenousStatus);
    }
    
    public void InitializeLayout()
    {
      GenerateMainGrid();
    }

    private void GenerateMainGrid()
    {
      this.SetGrid(5, 8);
      this.Margin = new Thickness(5);      
    }
  }
}
