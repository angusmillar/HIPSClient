using HIPSClient.HipsTinkerTool.AvalonEditSupport;
using HIPSClient.HipsTinkerTool.Controller;
using HIPSClient.HipsTinkerTool.Style;
using HIPSClient.HipsTinkerTool.View.Common;
using HIPSClient.HipsTinkerTool.ViewModel.Pathology;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace HIPSClient.HipsTinkerTool.View.Pathology
{
  public class PathologyTab : BaseValidationTabItem, IView
  {
    public PathologyVM PathologyVM { get; private set; }
    public PathologyController Controller { get; private set; }
    ICSharpCode.AvalonEdit.TextEditor HL7MessageTextEditor;
    private ToolTip toolTip;
    private Dictionary<PeterPiper.Hl7.V2.Schema.Model.VersionsSupported, PeterPiper.Hl7.V2.Schema.Support.SchemaSupport> oHl7V2SchemaSupportDic = new Dictionary<PeterPiper.Hl7.V2.Schema.Model.VersionsSupported, PeterPiper.Hl7.V2.Schema.Support.SchemaSupport>();

    public PathologyTab(PathologyVM PathologyVM)
      :base(PathologyVM)
    {
      this.PathologyVM = PathologyVM;
      Controller = new PathologyController(this);
      InitializeLayout();
    }

    public void InitializeLayout()
    {
      Header = "Pathology";
      Grid TabMainGrid = GenerateMainGrid();

      //The Main Tabs Content
      this.SetContent(TabMainGrid);
      

      //The ViewModel
      DataContext = PathologyVM;

      //Patient Group ----------------------------------------------
      var PatientGroup = new GroupBox();
      PatientGroup.Header = "Patient";
      Grid.SetRow(PatientGroup, 0);
      Grid.SetColumn(PatientGroup, 0);
      TabMainGrid.Children.Add(PatientGroup);
    
      Common.PatientGrid PatientGrid = new Common.PatientGrid(PathologyVM.Patient);
      PatientGroup.Content = PatientGrid;


      StackPanel PatIdAndHL7MessageGenStackPanel = new StackPanel();
      PatIdAndHL7MessageGenStackPanel.Orientation = Orientation.Vertical;
      Grid.SetRow(PatIdAndHL7MessageGenStackPanel, 1);
      Grid.SetColumn(PatIdAndHL7MessageGenStackPanel, 0);
      TabMainGrid.Children.Add(PatIdAndHL7MessageGenStackPanel);



      //Patient Identifier Group ----------------------------------------------
      var PatientIdentiferGroup = new GroupBox();
      PatientIdentiferGroup.Header = "Patient Identifiers";
      PatIdAndHL7MessageGenStackPanel.Children.Add(PatientIdentiferGroup);
      //Grid.SetRow(PatientIdentiferGroup, 1);
      //Grid.SetColumn(PatientIdentiferGroup, 0);
      //TabMainGrid.Children.Add(PatientIdentiferGroup);

      Common.PatientIdentifierGrid PatientIdentifierGrid = new Common.PatientIdentifierGrid(PathologyVM.PatientIdentifierList);
      PatientIdentiferGroup.Content = PatientIdentifierGrid;

      //HL7 Message Generation -------------------------------------------------
      var HL7MessageGenGroup = new GroupBox();
      HL7MessageGenGroup.Header = "HL7 Message Generation";
      PatIdAndHL7MessageGenStackPanel.Children.Add(HL7MessageGenGroup);

      Grid HL7MessageGenGrid = GlobalStyleManager.GetGrid(1, 4);
      HL7MessageGenGroup.Content = HL7MessageGenGrid;
     

      Button GenerateHL7MessageButton = new Button();
      GenerateHL7MessageButton.Content = "Generate Message";

      GenerateHL7MessageButton.SetBinding(Button.IsEnabledProperty, "OkToSave");
      GenerateHL7MessageButton.Margin = new Thickness(3);
      GenerateHL7MessageButton.Padding = new Thickness(3);
      Grid.SetRow(GenerateHL7MessageButton, 0);
      Grid.SetColumn(GenerateHL7MessageButton, 0);
      GenerateHL7MessageButton.Click += new RoutedEventHandler((Obj, e) =>
      {
        if (PathologyVM.OkToSave)
        {
          HL7MessageTextEditor.Text = PathologyVM.GetHL7Message();
        }
        else
        {
          HL7MessageTextEditor.Text = "Missing mandatory elements review input fields.";
        }
      });
      HL7MessageGenGrid.Children.Add(GenerateHL7MessageButton);

      //Order Group ----------------------------------------------
      var PathologyOrderGroup = new GroupBox();
      PathologyOrderGroup.Header = "Order";
      Grid.SetRow(PathologyOrderGroup, 0);
      Grid.SetColumn(PathologyOrderGroup, 1);
      TabMainGrid.Children.Add(PathologyOrderGroup);

      Common.PathologyOrderGrid PathologyOrderGrid = new Common.PathologyOrderGrid(PathologyVM.Order);
      PathologyOrderGroup.Content = PathologyOrderGrid;

      //Reports Group ----------------------------------------------
      var PatientReportsGroup = new GroupBox();
      PatientReportsGroup.Header = "Reports";
      Grid.SetRow(PatientReportsGroup, 1);
      Grid.SetColumn(PatientReportsGroup, 1);
      TabMainGrid.Children.Add(PatientReportsGroup);

      PatientReportsGroup.Content = new PathologyReportGrid(PathologyVM);

      //HL7 Message Group ----------------------------------------------
      var HL7MessageGroup = new GroupBox();
      HL7MessageGroup.Header = "HL7 Message";
      Grid.SetRow(HL7MessageGroup, 2);
      Grid.SetColumn(HL7MessageGroup, 0);
      Grid.SetColumnSpan(HL7MessageGroup, 2);
      TabMainGrid.Children.Add(HL7MessageGroup);

      Grid Hl7MessageGrid = new Grid();
      var Row1 = new RowDefinition();
      Row1.Height = new GridLength(30, GridUnitType.Auto);
      Hl7MessageGrid.RowDefinitions.Add(Row1);

      var Row2 = new RowDefinition();
      Hl7MessageGrid.RowDefinitions.Add(Row2);
      
      var Col = new ColumnDefinition();
      Hl7MessageGrid.ColumnDefinitions.Add(Col);
      

      HL7MessageGroup.Content = Hl7MessageGrid;

      if (!oHl7V2SchemaSupportDic.ContainsKey(PeterPiper.Hl7.V2.Schema.Model.Version.GetVersionFromString("2.4")))
      {
        PeterPiper.Hl7.V2.Schema.Support.SchemaSupport oSchema = new PeterPiper.Hl7.V2.Schema.Support.SchemaSupport();
        oSchema.LoadSchema("2.4", "ORU", "R01");
        if (oSchema.CurrentSchema != null)
          oHl7V2SchemaSupportDic.Add(oSchema.CurrentSchema.Version, oSchema);
      }

      HL7MessageTextEditor = new ICSharpCode.AvalonEdit.TextEditor();
      HL7MessageTextEditor.VerticalAlignment = VerticalAlignment.Center;
      ExtentionAvalonEdit.AvalonEditContextMenu(HL7MessageTextEditor);
      HL7MessageTextEditor.SetSyntaxType(AvalonEditSyntaxHighlightingTypes.HL7V2);
      HL7MessageTextEditor.Text = PathologyVM.GetHL7Message();

      HL7MessageTextEditor.MouseHoverStopped += HL7MessageTextEditor_MouseHoverStopped;
      HL7MessageTextEditor.TextChanged += HL7MessageTextEditor_TextChanged;
      HL7MessageTextEditor.MouseHover += HL7MessageTextEditor_MouseHover;
      
      
      Border Boarder = new Border();
      Boarder.BorderThickness = new Thickness(3);
      Boarder.BorderBrush = Brushes.LightGray;
      Grid.SetRow(Boarder, 1);
      Grid.SetColumn(Boarder, 0);
      Boarder.Child = HL7MessageTextEditor;

      Hl7MessageGrid.Children.Add(Boarder);
      
    }
    
    private void HL7MessageTextEditor_MouseHover(object sender, System.Windows.Input.MouseEventArgs e)
    {
      var pos = HL7MessageTextEditor.GetPositionFromPoint(e.GetPosition(HL7MessageTextEditor));

      if (pos != null && pos.Value.VisualColumn != 0)
      {
        toolTip = HL7MessageTextEditor.Document.GetHL7V2PostionDisplayInformation(pos.Value, oHl7V2SchemaSupportDic);
        if (toolTip != null)
        {
          toolTip.PlacementTarget = this; // required for property inheritance
          toolTip.IsOpen = true;
        }
        e.Handled = true;
      }

    }

    private void HL7MessageTextEditor_TextChanged(object sender, System.EventArgs e)
    {
      HL7MessageTextEditor.MouseHover -= HL7MessageTextEditor_MouseHover;
      HL7MessageTextEditor.MouseHoverStopped -= HL7MessageTextEditor_MouseHoverStopped;
      //This geting the Hash and compairing is dumb!
      //but I just cannot seem to prevent the double event being raised
      //when the inputbox is editied by the user.
      //Need to put more effort into it but for now a hash check is
      //about the best / fastest work-a-round
      //int CurrentTextHash = HL7MessageTextEditor.Text.GetHashCode();
      //if (OldInputTextHash != CurrentTextHash)
      //{
      //  OldInputTextHash = CurrentTextHash;

      //  HL7MessageTextEditor.Background = Brushes.White;
      //  TransformMessage();
      //}
      HL7MessageTextEditor.MouseHoverStopped += HL7MessageTextEditor_MouseHoverStopped;
      HL7MessageTextEditor.MouseHover += HL7MessageTextEditor_MouseHover;
    }

    private void HL7MessageTextEditor_MouseHoverStopped(object sender, System.Windows.Input.MouseEventArgs e)
    {
      if (toolTip != null)
        toolTip.IsOpen = false;
    }

    private Grid GenerateMainGrid()
    {
      Grid Grid = new Grid();

      var Row1 = new RowDefinition();
      Row1.Height = new GridLength(30, GridUnitType.Auto);
      Grid.RowDefinitions.Add(Row1);

      var Row2 = new RowDefinition();
      Row2.Height = new GridLength(30, GridUnitType.Auto);
      Grid.RowDefinitions.Add(Row2);

      var Row3 = new RowDefinition();
      Row3.Height = new GridLength(6, GridUnitType.Star);
      Grid.RowDefinitions.Add(Row3);

      var Row4 = new RowDefinition();
      Row4.Height = new GridLength(60, GridUnitType.Pixel);
      Grid.RowDefinitions.Add(Row4);


      var Col1 = new ColumnDefinition();
      Grid.ColumnDefinitions.Add(Col1);

      var Col2 = new ColumnDefinition();
      Grid.ColumnDefinitions.Add(Col2);

      return Grid;

      //return GlobalStyleManager.GetGrid(3, 2);
    }





  }
}
