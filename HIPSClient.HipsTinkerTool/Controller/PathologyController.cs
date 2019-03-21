using HIPSClient.HipsTinkerTool.View.Pathology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.HipsTinkerTool.Controller
{
  public class PathologyController
  {
    public PathologyTab PathologyTab { get; private set; }

    public PathologyController(PathologyTab PathologyTab)
    {
      this.PathologyTab = PathologyTab;
    }
   
  }
}
