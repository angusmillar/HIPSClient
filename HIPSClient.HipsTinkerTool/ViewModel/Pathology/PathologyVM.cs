using HIPSClient.Common.Tools.Enum;
using HIPSClient.Hips.Model;
using System.Collections.ObjectModel;
using HIPSClient.HipsTinkerTool.ViewModel.Common;

namespace HIPSClient.HipsTinkerTool.ViewModel.Pathology
{
  public class PathologyVM : BaseVM
  {
    private ADT Adt;
    public ObservableCollection<PatientIdentifierItemVM> PatientIdentifierList { get; set; }

    public PathologyVM()
    {
      Adt = new ADT();
      _PatientFamily = "Millar";
      PatientIdentifierList = new ObservableCollection<PatientIdentifierItemVM>()
      {
        new PatientIdentifierItemVM()
        {
           AssigningAuthority = "RBWH",
           Type = PatientIdentifierType.MedicalRecordNumber.ToString(),
           IdentifierType = PatientIdentifierType.MedicalRecordNumber.GetLiteral(),
           Value = "12345"
        },
        new PatientIdentifierItemVM()
        {
           AssigningAuthority = "AUSLAB",
           Type =  PatientIdentifierType.PatientInternalIdentifier.ToString(),
           IdentifierType = PatientIdentifierType.PatientInternalIdentifier.GetLiteral(),
           Value = "1234562"
        },
        new PatientIdentifierItemVM()
        {
           AssigningAuthority = PatientIdentifier.GetAssigningAuthorityForPatientIdentifierType(PatientIdentifierType.DVA),
           Type = PatientIdentifierType.DVA.ToString(),
           IdentifierType = PatientIdentifierType.DVA.GetLiteral(),
           Value = "NABC1234C"
        },
        new PatientIdentifierItemVM()
        {
           AssigningAuthority = PatientIdentifier.GetAssigningAuthorityForPatientIdentifierType(PatientIdentifierType.MedicareNumber),
           Type = PatientIdentifierType.MedicareNumber.ToString(),
           IdentifierType = PatientIdentifierType.MedicareNumber.GetLiteral(),
           Value = "61405230931"
        },
        new PatientIdentifierItemVM()
        {
           AssigningAuthority = PatientIdentifier.GetAssigningAuthorityForPatientIdentifierType(PatientIdentifierType.IHI),
           Type = PatientIdentifierType.IHI.ToString(),
           IdentifierType = PatientIdentifierType.IHI.GetLiteral(),
           Value = "8003608333428779"
        },
        new PatientIdentifierItemVM()
        {
           AssigningAuthority = PatientIdentifier.GetAssigningAuthorityForPatientIdentifierType(PatientIdentifierType.StatePatientId),
           Type = PatientIdentifierType.StatePatientId.ToString(),
           IdentifierType = PatientIdentifierType.StatePatientId.GetLiteral(),
           Value = "QLD123456"
        }

      };
    }

    private string _PatientFamily;
    public string PatientFamily
    {
      get
      {
        return _PatientFamily;
      }
      set
      {
        _PatientFamily = value;
        OnPropertyChanged("PatientFamily");
      }
    }


  }
}
