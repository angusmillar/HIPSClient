using HIPSClient.HipsTinkerTool.Controller;
using HIPSClient.HipsTinkerTool.Style;
using HIPSClient.HipsTinkerTool.ViewModel.Common;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public class PathologyOrderGrid : Grid, IView
  {
    public OrderVM OrderVM { get; private set; }
    
    public PathologyOrderGrid(OrderVM OrderVM)
    {
      this.OrderVM = OrderVM;
      InitializeLayout();
      GenerateControls();
    }

    private void GenerateControls()
    {
      int ColOneLabelWidth = 90;
      
      //Order Number
      DockPanel OrderNumber = GlobalStyleManager.GetValueParameterDockPanel("Order Number", ColOneLabelWidth, "Order.OrderNumber");
      Grid.SetColumn(OrderNumber, 0);
      Grid.SetColumnSpan(OrderNumber, 5);
      Grid.SetRow(OrderNumber, 0);
      this.Children.Add(OrderNumber);

      //Provider Name 
      DockPanel ProviderNameFormated = GlobalStyleManager.GetValueParameterDockPanel("Provider", ColOneLabelWidth, "Order.ProviderName.NameFormated", true);
      Grid.SetColumn(ProviderNameFormated, 0);
      Grid.SetColumnSpan(ProviderNameFormated, 5);
      Grid.SetRow(ProviderNameFormated, 1);
      this.Children.Add(ProviderNameFormated);

      Button EditProviderNameButton = new Button();
      EditProviderNameButton.Content = "Edit";
      EditProviderNameButton.Margin = new Thickness(3);
      EditProviderNameButton.Padding = new Thickness(3);
      Grid.SetColumn(EditProviderNameButton, 5);
      Grid.SetColumnSpan(EditProviderNameButton, 1);
      Grid.SetRow(EditProviderNameButton, 1);
      this.Children.Add(EditProviderNameButton);
      EditProviderNameButton.Click += new RoutedEventHandler((obj, e) =>
      {
        var ProviderNameEditWindow = new NameWindow(OrderVM.ProviderName);
        ProviderNameEditWindow.Title = "Edit Ordering Provider Name";
        ProviderNameEditWindow.Owner = Window.GetWindow(this);
        ProviderNameEditWindow.ShowDialog();
      });

      //Requested Date & Time Formatted
      DockPanel RequestedDateTimeFormatted = GlobalStyleManager.GetValueParameterDockPanel("Requested", ColOneLabelWidth, "Order.RequestedDateTime.DateTimeFormatted", true);
      Grid.SetColumn(RequestedDateTimeFormatted, 0);
      Grid.SetColumnSpan(RequestedDateTimeFormatted, 5);
      Grid.SetRow(RequestedDateTimeFormatted, 2);
      this.Children.Add(RequestedDateTimeFormatted);

      Button EditRequestDateTimeButton = new Button();
      EditRequestDateTimeButton.Content = "Edit";
      EditRequestDateTimeButton.Margin = new Thickness(3);
      EditRequestDateTimeButton.Padding = new Thickness(3);
      Grid.SetColumn(EditRequestDateTimeButton, 5);
      Grid.SetColumnSpan(EditRequestDateTimeButton, 1);
      Grid.SetRow(EditRequestDateTimeButton, 2);
      this.Children.Add(EditRequestDateTimeButton);
      EditRequestDateTimeButton.Click += new RoutedEventHandler((obj, e) =>
      {
        DateTimeWindow RequestedDateTimeWindow = new DateTimeWindow(OrderVM.RequestedDateTime);
        RequestedDateTimeWindow.Title = "Edit Requested Date & Time";
        RequestedDateTimeWindow.Owner = Window.GetWindow(this);
        RequestedDateTimeWindow.ShowDialog();
      });

      //Collection Date & Time Formatted
      DockPanel CollectionDateTimeFormatted = GlobalStyleManager.GetValueParameterDockPanel("Collection", ColOneLabelWidth, "Order.CollectionDateTime.DateTimeFormatted", true);
      Grid.SetColumn(CollectionDateTimeFormatted, 0);
      Grid.SetColumnSpan(CollectionDateTimeFormatted, 5);
      Grid.SetRow(CollectionDateTimeFormatted, 3);
      this.Children.Add(CollectionDateTimeFormatted);

      Button EditCollectionDateTimeButton = new Button();
      EditCollectionDateTimeButton.Content = "Edit";
      EditCollectionDateTimeButton.Margin = new Thickness(3);
      EditCollectionDateTimeButton.Padding = new Thickness(3);
      Grid.SetColumn(EditCollectionDateTimeButton, 5);
      Grid.SetColumnSpan(EditCollectionDateTimeButton, 1);
      Grid.SetRow(EditCollectionDateTimeButton, 3);
      this.Children.Add(EditCollectionDateTimeButton);
      EditCollectionDateTimeButton.Click += new RoutedEventHandler((obj, e) =>
      {
        DateTimeWindow CollectionDateTimeWindow = new DateTimeWindow(OrderVM.CollectionDateTime);
        CollectionDateTimeWindow.Title = "Edit Collection Date & Time";
        CollectionDateTimeWindow.Owner = Window.GetWindow(this);
        CollectionDateTimeWindow.ShowDialog();
      });

      
      
    }
    
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

      var Row0 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row1 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row2 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row3 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row4 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row5 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row6 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
      var Row7 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };

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
