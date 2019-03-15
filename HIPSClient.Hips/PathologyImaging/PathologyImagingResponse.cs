using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.PathologyImaging
{
  public enum PathologyImagingResponseStatus { None, OK, Warning, Error };
  public class PathologyImagingResponse
  {
    public PathologyImagingResponseStatus Status { get; set; }
    public string Message { get; set; }
  }
}
