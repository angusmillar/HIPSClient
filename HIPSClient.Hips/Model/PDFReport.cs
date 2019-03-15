using HIPSClient.Common.Tools.String;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.Model
{
  public class PDFReport
  {
    private string _FilePath;
    public string Filepath { set { _FilePath = value; } }
    
    private byte[] _PDFBtyes;
    public byte[] PDFBytes { set { _PDFBtyes = value; } }

    public ResultStatus ResultStatus { get; set; }

    internal byte[] GetPDF()
    {
      if (_PDFBtyes != null && !_FilePath.IsSet())
      {
        throw new Common.CustomException.HipsClientException("No PDF given to either PDF Bytes[] or Filepath. ");
      }
      else
      {
        if (_PDFBtyes != null)
        {
          return _PDFBtyes;
        }
        else
        {
          try
          {
            return File.ReadAllBytes(_FilePath);
          }
          catch (Exception exec)
          {
            throw new Common.CustomException.HipsClientException($"Unable to load PDF from file path {_FilePath}, see inner exception for more info", exec);
          }
        }
      }
    }
  }
}
