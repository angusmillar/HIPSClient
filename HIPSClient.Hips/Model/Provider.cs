using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.Model
{
  public class Provider
  {
    public string Family { get; set; }
    public string Given { get; set; }
    public string Title { get; set; }
    public ProviderIdentifier Identifer { get; set; }
  }
}
