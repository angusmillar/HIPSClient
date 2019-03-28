using HIPSClient.HipsTinkerTool.View;
using HIPSClient.HipsTinkerTool.View.Main;
using System.Windows;

namespace HIPSClient.HipsTinkerTool
{
  public  class MainWindow : Window, IView
  {
    public MainWindow()
    {
      Title = "HIPS Tinker Tool";
      Width = 1024;
      Height = 768;
      MinWidth = 1024;
      MinHeight = 768;      
      InitializeLayout();
    }

    public void InitializeLayout()
    {
      Content = new MainWindowsTab();
    }
  }
}