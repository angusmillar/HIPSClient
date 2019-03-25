using HIPSClient.HipsTinkerTool.Controller;
using HIPSClient.HipsTinkerTool.View.ADT;
using HIPSClient.HipsTinkerTool.View.Pathology;
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
      Items.Add(new PathologyTab());
      Items.Add(new ADTTab());
    }
  }
}
