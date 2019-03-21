using HIPSClient.Hips.Model;
using System.Collections.ObjectModel;

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
           AssigningAuthority = "AssginAuth",
           Type = "Medicare",
           IdentifierType = "MC",
           Value = "12345"
        },
        new PatientIdentifierItemVM()
        {
           AssigningAuthority = "AssginAuth2",
           Type = "IHI",
           IdentifierType = "MC2",
           Value = "1234562"
        },
        new PatientIdentifierItemVM()
        {
           AssigningAuthority = "AssginAuth2",
           Type = "DVA",
           IdentifierType = "MC2",
           Value = "4B123456"
        },
        new PatientIdentifierItemVM()
        {
           AssigningAuthority = "AssginAuth2",
           Type = "MR",
           IdentifierType = "MC2",
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
