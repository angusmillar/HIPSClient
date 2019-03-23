using HIPSClient.HipsTinkerTool.View.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.HipsTinkerTool.Controller
{
  public class PatientIdentifierController
  {
    public PatientIdentifierGrid PatientIdentifierGrid { get; private set; }

    public PatientIdentifierController(PatientIdentifierGrid PatientIdentifierGrid)
    {
      this.PatientIdentifierGrid = PatientIdentifierGrid;
    }

    public void OnPatientIdentifierItemSelection()
    {
      Console.WriteLine($"You clicked it ");
    }

    public void OnPatientIdentifierItemRemove(ViewModel.Common.PatientIdentifierItemVM Item)
    {
      PatientIdentifierGrid.PatientIdentifierList.Remove(Item);
      Console.WriteLine($"You clicked Remove ");
    }

    public void OnPatientIdentifierItemAdd(ViewModel.Common.PatientIdentifierItemVM Item)
    {
      PatientIdentifierGrid.PatientIdentifierList.Add(Item);
      Console.WriteLine($"You clicked Remove ");
    }
  }
}
