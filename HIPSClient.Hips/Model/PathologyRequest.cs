using HIPSClient.Common.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.Model
{
  public enum ResultStatus
  {
    [EnumUIDisplay("Final")]
    [EnumLiteral("F")]
    Final,
    [EnumUIDisplay("Corrected")]
    [EnumLiteral("C")]
    Corrected,
    [EnumUIDisplay("Deleted")]
    [EnumLiteral("X")]
    Delete
  }

  public class PathologyRequest
  {
    /// <summary>
    /// OBR-2
    /// </summary>
    public string ReportIdentifier { get; set; }

    /// <summary>
    /// OBR-4
    /// </summary>
    public UniversalServiceIdentifier ReportName { get; set; }
    
    /// <summary>
    /// OBR-32
    /// </summary>
    public Provider DocumentAuthor { get; set; }    

    /// <summary>
    /// OBR-22 (When the report was released)
    /// </summary>
    public DateTimeOffset ReportedDateTime { get; set; }

    /// <summary>
    /// OBR-24 Diagnostic Serv Sect ID 0074 
    /// </summary>
    public string DepartmentCode { get; set; }

    /// <summary>
    /// OBR-25 ResultStatus
    /// </summary>
    public ResultStatus ReportStatus { get; set; }
  }
}
