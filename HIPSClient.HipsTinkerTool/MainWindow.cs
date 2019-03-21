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
      MinWidth = 800;
      MinHeight = 600;      
      InitializeLayout();
    }

    public void InitializeLayout()
    {
      Content = new MainWindowsTab();
    }
  }
}