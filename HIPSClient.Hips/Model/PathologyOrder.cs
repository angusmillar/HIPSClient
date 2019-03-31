using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.Model
{
  public class PathologyOrder
  {
    /// <summary>
    /// OBR-3
    /// </summary>
    public string OrderIdentifier { get; set; }

    /// <summary>
    /// OBR-27.4. or ORC-9 (This client uses use OBR-27.4)
    /// </summary>
    public DateTimeOffset OrderedDateTime { get; set; }

    /// <summary>
    /// OBR-7
    /// </summary>
    public DateTimeOffset CollectionDateTime { get; set; }

    /// <summary>
    /// OBR-16
    /// </summary>
    public Provider OrderingProvider { get; set; }

    /// <summary>
    /// OBR-20
    /// Used for existence of My Health Record, HIPS uses this field to
    /// determine if a ‘DoesPCEHRExist’ operation is required, and standing
    /// consent has been met.
    /// </summary>
    public bool? IsMyHealthRecordDisclosed { get; set; }


  }
}
