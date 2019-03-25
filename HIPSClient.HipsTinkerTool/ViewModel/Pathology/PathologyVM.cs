using HIPSClient.Common.Tools.Enum;
using HIPSClient.Hips.Model;
using System.Collections.ObjectModel;
using HIPSClient.HipsTinkerTool.ViewModel.Common;

namespace HIPSClient.HipsTinkerTool.ViewModel.Pathology
{
  public class PathologyVM : BaseVM
  {
    private ORU Oru;
    public ObservableCollection<PatientIdentifierItemVM> PatientIdentifierList { get; set; }
    public PatientVM Patient { get; set; }

    public PathologyVM()
    {
      Oru = new ORU();
      
      PatientIdentifierList = new ObservableCollection<PatientIdentifierItemVM>()
      {
        new PatientIdentifierItemVM()
        {
           AssigningAuthority = "RBWH",
           Type = PatientIdentifierType.MedicalRecordNumber.GetUIDisplay(),
           IdentifierType = PatientIdentifierType.MedicalRecordNumber.GetLiteral(),
           Value = "12345"
        },
        new PatientIdentifierItemVM()
        {
           AssigningAuthority = "AUSLAB",
           Type =  PatientIdentifierType.PatientInternalIdentifier.GetUIDisplay(),
           IdentifierType = PatientIdentifierType.PatientInternalIdentifier.GetLiteral(),
           Value = "1234562"
        },
        new PatientIdentifierItemVM()
        {
           AssigningAuthority = PatientIdentifier.GetAssigningAuthorityForPatientIdentifierType(PatientIdentifierType.DVA),
           Type = PatientIdentifierType.DVA.GetUIDisplay(),
           IdentifierType = PatientIdentifierType.DVA.GetLiteral(),
           Value = "NABC1234C"
        },
        new PatientIdentifierItemVM()
        {
           AssigningAuthority = PatientIdentifier.GetAssigningAuthorityForPatientIdentifierType(PatientIdentifierType.MedicareNumber),
           Type = PatientIdentifierType.MedicareNumber.GetUIDisplay(),
           IdentifierType = PatientIdentifierType.MedicareNumber.GetLiteral(),
           Value = "61405230931"
        },
        new PatientIdentifierItemVM()
        {
           AssigningAuthority = PatientIdentifier.GetAssigningAuthorityForPatientIdentifierType(PatientIdentifierType.IHI),
           Type = PatientIdentifierType.IHI.GetUIDisplay(),
           IdentifierType = PatientIdentifierType.IHI.GetLiteral(),
           Value = "8003608333428779"
        },
        new PatientIdentifierItemVM()
        {
           AssigningAuthority = PatientIdentifier.GetAssigningAuthorityForPatientIdentifierType(PatientIdentifierType.StatePatientId),
           Type = PatientIdentifierType.StatePatientId.GetUIDisplay(),
           IdentifierType = PatientIdentifierType.StatePatientId.GetLiteral(),
           Value = "QLD123456"
        }

      };
      Patient = new PatientVM()
      {
        Title = "Mr",
        FamilyName = "Millar",
        GivenName = "Angus",
        DateOfBirth = new System.DateTime(1973, 09, 30),
        Gender = HIPSClient.Hips.Model.Gender.Male.GetUIDisplay(),
        Address = new AddressVM()
        {
          AddressLineOne = "4 Norman Street",
          AddressLineTwo = "Line 2",
          Suburb = "WembleyDowns",
          PostCode = "4017",
          State = "W.A",
          Country = "AUS"
        },
         HomePhone = "08 9341 2041",
         WorkPhone = "0481 059995"        
      };
      
    }
    
  }
}
