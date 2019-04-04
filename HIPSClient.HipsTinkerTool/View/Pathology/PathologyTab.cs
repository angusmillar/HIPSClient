using HIPSClient.HipsTinkerTool.AvalonEditSupport;
using HIPSClient.HipsTinkerTool.Controller;
using HIPSClient.HipsTinkerTool.Style;
using HIPSClient.HipsTinkerTool.View.Common;
using HIPSClient.HipsTinkerTool.ViewModel.Pathology;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HIPSClient.HipsTinkerTool.View.Pathology
{
  public class PathologyTab : TabItem, IView
  {
    public PathologyVM PathologyVM { get; private set; }
    public PathologyController Controller { get; private set; }
    ICSharpCode.AvalonEdit.TextEditor HL7MessageTextEditor;
    private ToolTip toolTip;
    private Dictionary<PeterPiper.Hl7.V2.Schema.Model.VersionsSupported, PeterPiper.Hl7.V2.Schema.Support.SchemaSupport> oHl7V2SchemaSupportDic = new Dictionary<PeterPiper.Hl7.V2.Schema.Model.VersionsSupported, PeterPiper.Hl7.V2.Schema.Support.SchemaSupport>();

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

      var HL7MessageGroup = new GroupBox();
      HL7MessageGroup.Header = "HL7 Message";
      Grid.SetRow(HL7MessageGroup, 2);
      Grid.SetColumn(HL7MessageGroup, 0);
      Grid.SetColumnSpan(HL7MessageGroup, 2);
      TabMainGrid.Children.Add(HL7MessageGroup);

      if (!oHl7V2SchemaSupportDic.ContainsKey(PeterPiper.Hl7.V2.Schema.Model.Version.GetVersionFromString("2.4")))
      {
        PeterPiper.Hl7.V2.Schema.Support.SchemaSupport oSchema = new PeterPiper.Hl7.V2.Schema.Support.SchemaSupport();
        oSchema.LoadSchema("2.4", "ORU", "R01");
        if (oSchema.CurrentSchema != null)
          oHl7V2SchemaSupportDic.Add(oSchema.CurrentSchema.Version, oSchema);
      }

      HL7MessageTextEditor = new ICSharpCode.AvalonEdit.TextEditor();
      ExtentionAvalonEdit.AvalonEditContextMenu(HL7MessageTextEditor);
      HL7MessageTextEditor.SetSyntaxType(AvalonEditSyntaxHighlightingTypes.HL7V2);
      HL7MessageTextEditor.Text = @"MSH|^~\&|SUPER-LIS^2.16.840.1.113883.19.1^ISO|NEHTAPATH^4321^AUSNATA|Rhubarb-EMR^2.16.840.1.113883.19.4.2^ISO|NEHTAHOSP^2.16.840.1.113883.19.5^ISO|201504101120+1000||ORU^R01^ORU_R01|P0000051504102331070|P|2.4|||AL|NE|AUS|8859/1";

      HL7MessageTextEditor.MouseHoverStopped += HL7MessageTextEditor_MouseHoverStopped;
      HL7MessageTextEditor.TextChanged += HL7MessageTextEditor_TextChanged;
      HL7MessageTextEditor.MouseHover += HL7MessageTextEditor_MouseHover;



      Grid.SetRow(HL7MessageTextEditor, 2);
      Grid.SetColumn(HL7MessageTextEditor, 0);
      HL7MessageGroup.Content = HL7MessageTextEditor;



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
      return GlobalStyleManager.GetGrid(3, 2);
    }





  }
}
