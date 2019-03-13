using HIPSClient.Common.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Hips.Model
{
  public class Address
  {
    public string AddressLineOne { get; set; }
    public string AddressLineTwo { get; set; }
    public string Suburb { get; set; }
    public string PostCode { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
  }
}
