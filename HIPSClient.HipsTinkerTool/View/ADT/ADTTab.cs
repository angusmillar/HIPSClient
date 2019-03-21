using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HIPSClient.HipsTinkerTool.View.ADT
{
  public class ADTTab : TabItem, IView
  {
    public ADTTab()
    {
      InitializeLayout();
    }

    public void InitializeLayout()
    {
      Header = "ADT";
    }
  }
}
