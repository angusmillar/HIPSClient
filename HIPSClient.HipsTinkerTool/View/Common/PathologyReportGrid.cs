using HIPSClient.Common.Tools.Enum;
using HIPSClient.Hips.Model;
using HIPSClient.HipsTinkerTool.Controller;
using HIPSClient.HipsTinkerTool.Style;
using HIPSClient.HipsTinkerTool.ViewModel.Common;
using HIPSClient.HipsTinkerTool.ViewModel.Pathology;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HIPSClient.HipsTinkerTool.View.Common
{
  public class PathologyReportGrid : Grid, IView
  {
    public PathologyVM PathologyVM { get; private set; }
    public PathologyReportGrid(PathologyVM PathologyVM)
    {
      this.PathologyVM = PathologyVM;
      InitializeLayout();
    }

    public void InitializeLayout()
    {
      this.SetGrid(4, 8);
     
      //Provider Name     
      DockPanel AuthorNameFormated = GlobalStyleManager.GetValueParameterDockPanel("Reporting Pathologist", 130, "AuthorName.NameFormated", true);
      Grid.SetRow(AuthorNameFormated, 0);
      Grid.SetColumn(AuthorNameFormated, 0);
      Grid.SetColumnSpan(AuthorNameFormated, 7);
      this.Children.Add(AuthorNameFormated);

      Button EditAuthorNameButton = GlobalStyleManager.GetButton("Edit");
      EditAuthorNameButton.Width = 55;
      EditAuthorNameButton.Click += new RoutedEventHandler((obj, e) =>
      {
        var AuthorNameEditWindow = new NameWindow(PathologyVM.AuthorName);
        AuthorNameEditWindow.Title = "Edit Author Name";
        AuthorNameEditWindow.Owner = Window.GetWindow(this);
        AuthorNameEditWindow.ShowDialog();
      });
      Grid.SetRow(EditAuthorNameButton, 0);
      Grid.SetColumn(EditAuthorNameButton, 7);
      Grid.SetColumnSpan(EditAuthorNameButton, 1);
      this.Children.Add(EditAuthorNameButton);


      Common.PathologyReportListGrid PathologyReportsList = new Common.PathologyReportListGrid(PathologyVM.PathologyRequestList);
      Grid.SetRow(PathologyReportsList, 1);
      Grid.SetColumn(PathologyReportsList, 0);
      Grid.SetColumnSpan(PathologyReportsList, 8);
      this.Children.Add(PathologyReportsList);

      DockPanel PDFFilePath = GlobalStyleManager.GetValueParameterDockPanel("PDF Report", 75, "PdfFilePath", true);
      Grid.SetRow(PDFFilePath, 2);
      Grid.SetColumn(PDFFilePath, 0);
      Grid.SetColumnSpan(PDFFilePath, 7);
      this.Children.Add(PDFFilePath);

      var SelectButton = GlobalStyleManager.GetButton("Select");
      SelectButton.Width = 55;
      SelectButton.HorizontalAlignment = HorizontalAlignment.Left;
      SelectButton.Click += new RoutedEventHandler((obj, e) =>
      {
        // Create OpenFileDialog
        Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

        // Launch OpenFileDialog by calling ShowDialog method
        Nullable<bool> result = openFileDlg.ShowDialog();
        // Get the selected file name and display in a TextBox.
        // Load content of file in a TextBlock
        if (result == true)
        {
          PathologyVM.PdfFilePath = openFileDlg.FileName;
          //FileNameTextBox.Text = openFileDlg.FileName;
          //TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
        }
      });
      Grid.SetRow(SelectButton, 2);
      Grid.SetColumn(SelectButton, 7);
      Grid.SetColumnSpan(SelectButton, 1);
      this.Children.Add(SelectButton);

      
    }
    
    
  }
}
