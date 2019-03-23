﻿using HIPSClient.Common.Tools.String;
using HIPSClient.HipsTinkerTool.Style;
using HIPSClient.HipsTinkerTool.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public class PatientIdentifierEditWindow : Window, IView
  {
    public PatientIdentifierItemVM PatientIdentifierItem { get; private set; }
    public PatientIdentifierItemVM TempPatientIdentifierItem { get; private set; }
    private bool CanSave()
    {
      return _ErrorCount == 0;
    }
    private int _ErrorCount;
    private Grid _MainGrid;
    private TextBox _ErrorMessage;
    private Border _ErrorBorder;
    

    public PatientIdentifierEditWindow(PatientIdentifierItemVM PatientIdentifierItem)
    {
      this.PatientIdentifierItem = PatientIdentifierItem;

      //This is a copy of the unchanged data in case they cancel the edit operation
      TempPatientIdentifierItem = new PatientIdentifierItemVM();
      PatientIdentifierItemMapper(this.PatientIdentifierItem, TempPatientIdentifierItem);
     

      InitializeLayout();


      _MainGrid = GenerateMainGrid();

      GeneratePropteryValues(_MainGrid);

      var ButtonStack = GenerateButtonStackPanel();   
      _MainGrid.Children.Add(ButtonStack);

      Border MainContentBoarder = new Border();
      MainContentBoarder.Background = Brushes.GhostWhite;
      MainContentBoarder.BorderBrush = Brushes.Silver;
      MainContentBoarder.BorderThickness = new Thickness(3);
      MainContentBoarder.Child = _MainGrid;
      DockPanel.SetDock(MainContentBoarder, System.Windows.Controls.Dock.Top);

      GenerateErrorMessageDisplay();

      DockPanel Dock = new DockPanel();
      Dock.LastChildFill = true;
      Dock.Children.Add(MainContentBoarder);
      Dock.Children.Add(_ErrorBorder);

      Content = Dock;
    }

    private void GenerateErrorMessageDisplay()
    {
      _ErrorMessage = new TextBox();
      _ErrorMessage.VerticalAlignment = VerticalAlignment.Stretch;
      _ErrorMessage.HorizontalAlignment = HorizontalAlignment.Stretch;
      _ErrorMessage.BorderThickness = new Thickness(0);
      _ErrorMessage.AcceptsReturn = true;
      _ErrorMessage.IsReadOnly = true;
      _ErrorMessage.TextWrapping =  TextWrapping.Wrap;
      _ErrorMessage.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

      Binding Bind = new Binding("ErrorMessage");
      Bind.Mode = BindingMode.OneWay;        
      _ErrorMessage.SetBinding(TextBox.TextProperty, Bind);
      

      _ErrorBorder = new Border();
      _ErrorBorder.Background = Brushes.GhostWhite;
      _ErrorBorder.BorderBrush = Brushes.Red;
      _ErrorBorder.BorderThickness = new Thickness(1);
      _ErrorBorder.Child = _ErrorMessage;
      _ErrorBorder.Visibility = Visibility.Collapsed;
      DockPanel.SetDock(_ErrorBorder, System.Windows.Controls.Dock.Bottom);
    }

    private Button SavePatientIdButton;

    public void InitializeLayout()
    {
      Title = "Patient Identifier";
      Width = 400;
      Height = 215;
      ResizeMode = ResizeMode.NoResize;
      WindowStyle = WindowStyle.None;
      DataContext = PatientIdentifierItem;
      WindowStartupLocation = WindowStartupLocation.CenterOwner;
      AddHandler(Validation.ErrorEvent, new RoutedEventHandler(OnErrorEvent));
    }

    private Grid GenerateMainGrid()
    {
      var Col0 = new ColumnDefinition();


      var Row0 = new RowDefinition();
      var Row1 = new RowDefinition();
      var Row2 = new RowDefinition();
      var Row3 = new RowDefinition();
      var Row4 = new RowDefinition();
      var Row5 = new RowDefinition();

      var Grid = new Grid();

      Grid.ColumnDefinitions.Add(Col0);


      Grid.RowDefinitions.Add(Row0);
      Grid.RowDefinitions.Add(Row1);
      Grid.RowDefinitions.Add(Row2);
      Grid.RowDefinitions.Add(Row3);
      Grid.RowDefinitions.Add(Row4);
      Grid.RowDefinitions.Add(Row5);

      return Grid;
    }

    private void GeneratePropteryValues(Grid MainGrid)
    {
      int LabelWidth = 150;
      
      List<string> ItemsSource = HIPSClient.Common.Tools.Enum.EnumUtility.ConvertEnumToDictionary<HIPSClient.Hips.Model.PatientIdentifierType>().Select(x => x.Key).ToList();
      var IdType = GlobalStyleManager.GetValueComboBoxEnumDockPanel("Type", LabelWidth, "Type", ItemsSource);
      Grid.SetRow(IdType, 0);
      Grid.SetColumn(IdType, 0);
      MainGrid.Children.Add(IdType);

      var IdIdentifierType = GlobalStyleManager.GetValueParameterDockPanel("Identifier Type", LabelWidth, "IdentifierType", true);
      Grid.SetRow(IdIdentifierType, 1);
      Grid.SetColumn(IdIdentifierType, 0);
      MainGrid.Children.Add(IdIdentifierType);

      var IdAssigingAuth = GlobalStyleManager.GetValueParameterDockPanel("Assigning Authority", LabelWidth, "AssigningAuthority");
      Grid.SetRow(IdAssigingAuth, 2);
      Grid.SetColumn(IdAssigingAuth, 0);
      MainGrid.Children.Add(IdAssigingAuth);

      var IdValue = GlobalStyleManager.GetValueParameterDockPanel("Value", LabelWidth, "Value");
      Grid.SetRow(IdValue, 3);
      Grid.SetColumn(IdValue, 0);
      MainGrid.Children.Add(IdValue);
      
    }
    
    private void OnErrorEvent(object sender, RoutedEventArgs e)
    {
      var validationEventArgs = e as ValidationErrorEventArgs;
      if (validationEventArgs == null)
      {
        throw new Exception("Unexpected ValidationErrorEventArgs equals null");
      }
      var x = validationEventArgs.Error;
      switch (validationEventArgs.Action)
      {
        case ValidationErrorEventAction.Added:         
          _ErrorCount++;       
          break;
        case ValidationErrorEventAction.Removed:         
          _ErrorCount--;
          break;
        default:
          throw new Exception($"Unexpected Action of {validationEventArgs.Action.ToString()}");          
      }

      //Set the Save button visibility based on error count
      SavePatientIdButton.IsEnabled = CanSave();

      //Hide the Error Message display.
      if (CanSave())
      {
        _ErrorBorder.Visibility = Visibility.Collapsed;
      }
      else
      {
        _ErrorBorder.Visibility = Visibility.Visible;
      }
    }

    private StackPanel GenerateButtonStackPanel()
    {
      SavePatientIdButton = new Button();
      SavePatientIdButton.Content = "Save";
      SavePatientIdButton.Width = 60;
      SavePatientIdButton.HorizontalAlignment = HorizontalAlignment.Right;
      SavePatientIdButton.Margin = new Thickness(2);
      //SavePatientIdButton.IsEnabled = CanSave;
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
        PatientIdentifierItemMapper(TempPatientIdentifierItem, this.PatientIdentifierItem);
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

    private void PatientIdentifierItemMapper(PatientIdentifierItemVM From, PatientIdentifierItemVM To)
    {
      To.AssigningAuthority = From.AssigningAuthority;
      To.IdentifierType = From.IdentifierType;
      To.Type = From.Type;
      To.Value = From.Value;
    }

    
  }
}
