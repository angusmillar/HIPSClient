using HIPSClient.HipsTinkerTool.Controller;
using HIPSClient.HipsTinkerTool.ViewModel.Pathology;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public class PatientIdentifierGrid : Grid, IView
  {
    public ObservableCollection<PatientIdentifierItemVM> PatientIdentifierList { get; private set; }
    public PatientIdentifierController Controller { get; private set; }
    private ListBox IdentiferListView;

    public PatientIdentifierGrid(ObservableCollection<PatientIdentifierItemVM> PatientIdentifierList)
    {
      this.PatientIdentifierList = PatientIdentifierList;
      this.Controller = new PatientIdentifierController(this);
      InitializeLayout();
    }

    public void InitializeLayout()
    {
      GenerateMainGrid();
    
      IdentiferListView = GeneratePatientIdentifierListBox();
      Grid.SetRow(IdentiferListView, 0);
      Grid.SetColumn(IdentiferListView, 0);
      
      this.Children.Add(IdentiferListView);

      StackPanel ButtonStack = GenerateButtonStackPanel();
      Grid.SetRow(ButtonStack, 1);
      Grid.SetColumn(ButtonStack, 0);
      this.Children.Add(ButtonStack);
      
    }

    private StackPanel GenerateButtonStackPanel()
    {
      Button RemovePatientIdButton = new Button();
      RemovePatientIdButton.Content = "Remove";
      RemovePatientIdButton.Width = 60;
      RemovePatientIdButton.HorizontalAlignment = HorizontalAlignment.Right;
      RemovePatientIdButton.Margin = new Thickness(2);
      RemovePatientIdButton.Click += new RoutedEventHandler((obj, e) => {
        if (IdentiferListView.SelectedItem is PatientIdentifierItemVM Item)
        {
          Controller.OnPatientIdentifierItemRemove(Item);
        }
      });


      Button EditPatientIdButton = new Button();
      EditPatientIdButton.Content = "Edit";
      EditPatientIdButton.Width = 60;
      EditPatientIdButton.HorizontalAlignment = HorizontalAlignment.Right;
      EditPatientIdButton.Margin = new Thickness(2);
      EditPatientIdButton.Click += new RoutedEventHandler((obj, e) => {
        if (IdentiferListView.SelectedItem is PatientIdentifierItemVM Item)
        {
          //Controller.OnPatientIdentifierItemRemove(Item);
        }
      });

      Button AddPatientIdButon = new Button();
      AddPatientIdButon.Content = "Add";
      AddPatientIdButon.Width = 60;
      AddPatientIdButon.HorizontalAlignment = HorizontalAlignment.Right;
      AddPatientIdButon.Margin = new Thickness(2);
      AddPatientIdButon.Click += new RoutedEventHandler((obj, e) => {
        if (IdentiferListView.SelectedItem is PatientIdentifierItemVM Item)
        {
          //Controller.OnPatientIdentifierItemRemove(Item);
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
      TypeTextBoxFactory.SetValue(Label.WidthProperty, 80.0);     
      TypeTextBoxFactory.SetValue(Label.FontWeightProperty, FontWeights.DemiBold);
      TypeTextBoxFactory.SetBinding(Label.ContentProperty, new Binding("Type"));

      var AssigningAuthorityTextBoxFactory = new FrameworkElementFactory(typeof(Label));      
      AssigningAuthorityTextBoxFactory.SetValue(Label.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
      AssigningAuthorityTextBoxFactory.SetBinding(Label.ContentProperty, new Binding("Value"));

      var StackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
      StackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
      StackPanelFactory.SetValue(StackPanel.HorizontalAlignmentProperty, HorizontalAlignment.Center);
      
      StackPanelFactory.AppendChild(TypeTextBoxFactory);
      StackPanelFactory.AppendChild(AssigningAuthorityTextBoxFactory);
     
      DataTemplate Template = new DataTemplate();
      Template.VisualTree = StackPanelFactory;

      ListBox ResultListBox = new ListBox();      
      ResultListBox.HorizontalAlignment = HorizontalAlignment.Stretch;      
      ResultListBox.Height = 100;      
      ResultListBox.Margin = new Thickness(2);
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

      var Grid = new Grid();
     
      this.ColumnDefinitions.Add(Col1);
      

      this.RowDefinitions.Add(Row1);
      this.RowDefinitions.Add(Row2);
          
    }
  }
}
