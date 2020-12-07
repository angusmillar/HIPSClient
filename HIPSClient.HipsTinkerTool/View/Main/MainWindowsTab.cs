using HIPSClient.HipsTinkerTool.Controller;
using HIPSClient.HipsTinkerTool.View.ADT;
using HIPSClient.HipsTinkerTool.View.Pathology;
using HIPSClient.HipsTinkerTool.ViewModel.Pathology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HIPSClient.HipsTinkerTool.View.Main
{
  public class MainWindowsTab: TabControl, IView
  {  
    public MainWindowsTab()
    {      
      InitializeLayout();
    }

    public void InitializeLayout()
    {      
      var test = new PathologyVM();
      Items.Add(new PathologyTab(test));
      Items.Add(new ADTTab());
      test.Initalise();
    }
  }
}
