using HIPSClient.HipsTinkerTool.View.Common;

namespace HIPSClient.HipsTinkerTool.Controller
{
  public class PatientIdentifierController
  {
    public PatientIdentifierGrid PatientIdentifierGrid { get; private set; }

    public PatientIdentifierController(PatientIdentifierGrid PatientIdentifierGrid)
    {
      this.PatientIdentifierGrid = PatientIdentifierGrid;
    }    

    public void OnPatientIdentifierItemRemove(ViewModel.Common.PatientIdentifierItemVM Item)
    {
      PatientIdentifierGrid.PatientIdentifierList.Remove(Item);     
    }

    public void OnPatientIdentifierItemAdd(ViewModel.Common.PatientIdentifierItemVM Item)
    {
      PatientIdentifierGrid.PatientIdentifierList.Add(Item);      
    }
  }
}
