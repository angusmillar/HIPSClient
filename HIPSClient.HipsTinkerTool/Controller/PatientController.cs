using HIPSClient.HipsTinkerTool.View.Common;

namespace HIPSClient.HipsTinkerTool.Controller
{
  public class PatientController
  {
    public PatientGrid PatientGrid { get; private set; }

    public PatientController(PatientGrid PatientGrid)
    {
      this.PatientGrid = PatientGrid;
    }    

    public void OnPatientIdentifierItemRemove(ViewModel.Common.PatientIdentifierItemVM Item)
    {
      //PatientIdentifierGrid.PatientIdentifierList.Remove(Item);     
    }

    public void OnPatientIdentifierItemAdd(ViewModel.Common.PatientIdentifierItemVM Item)
    {
      //PatientIdentifierGrid.PatientIdentifierList.Add(Item);      
    }
  }
}
