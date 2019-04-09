using HIPSClient.Common.Tools.Enum;
using HIPSClient.Hips.Model;
using HIPSClient.HipsTinkerTool.ViewModel.Common;
using System;
using System.Collections.ObjectModel;

namespace HIPSClient.HipsTinkerTool.ViewModel.Pathology
{
  public class PathologyVM : BaseValidationVM
  {
    private ORU Oru;
    public ObservableCollection<PatientIdentifierItemVM> PatientIdentifierList { get; set; }
    public PatientVM Patient { get; set; }

    private DateTime? _PatientDateOfBirth;
    public DateTime? PatientDateOfBirth
    {
      get { return _PatientDateOfBirth; }
      set
      {
        _PatientDateOfBirth = value;
        OnPropertyChanged("PatientDateOfBirth");
      }
    }


    public OrderVM Order { get; set; }
    public ObservableCollection<PathologyReportItemVM> PathologyRequestList { get; set; }
    public NameVM AuthorName { get; set; }

    private string _PdfFilePath;
    public string PdfFilePath
    {
      get
      {
        return _PdfFilePath;
      }
      set
      {
        _PdfFilePath = value;
        OnPropertyChanged("PdfFilePath");
      }
    }

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
        PatientName = new NameVM()
        {
          Family = "MIllar",
          Given = "Angus",
          Title = "Mr"
        },
        PatientDateOfBirth = new System.DateTime(1973, 09, 30),
        GenderFormatted = HIPSClient.Hips.Model.Gender.Male.GetUIDisplay(),
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
        WorkPhone = "0481 059995",
        IndigenousStatus = IndigenousStatusType.NeitherAboriginalNorTorresStraitIslanderOrigin.GetUIDisplay()
      };
      Order = new OrderVM()
      {
        OrderNumber = "P654321",
        RequestedDateTime = new DateTimeVM()
        {
          Date = new System.DateTime(2019, 01, 25),
          TimeFormatted = "10:00 AM",
          TimeZoneFormatted = "-1000"
        },
        CollectionDateTime = new DateTimeVM()
        {
          Date = new System.DateTime(2019, 01, 25),
          TimeFormatted = "1:30 PM",
          TimeZoneFormatted = "+1200"
        },
        ProviderName = new NameVM()
        {
          Family = "Blackwell",
          Given = "Scott",
          Title = "Dr"
        },
        IsMyHealthRecordDisclosed = true
      };

      PathologyRequestList = new ObservableCollection<PathologyReportItemVM>()
      {
        new PathologyReportItemVM()
        {
           ReportIdentifier = "19P123456",
           LocalCode = "FBE",
           LocalDescription = "Full Blood Examination",
           LocalSystemCode = "ADHA",
           SnomedCode = "12345678",
           SnomedPreferredTerm = "Full Blood Count",
           DepartmentCode = "HM",
           ReportedDateTime = new DateTimeVM()
           {
              Date = new System.DateTime(2019, 01, 12),
              TimeFormatted = "10:30 AM",
              TimeZoneFormatted = "+1000",
              IsTimeOptional = true
               

           },          
           ReportStatus = "Final"
        },
        new PathologyReportItemVM()
        {
           ReportIdentifier = "19P654321",
           LocalCode = "IM",
           LocalDescription = "Infectious Mononucleosis",
           LocalSystemCode = "ADHA",
           SnomedCode = "87654321",
           SnomedPreferredTerm = "Infectious Mononucleosis",
           DepartmentCode = "HM",          
           ReportedDateTime = new DateTimeVM()
           {
              Date = new System.DateTime(2019, 01, 12),
              TimeFormatted = "10:35 AM",
              TimeZoneFormatted = "+1000",
              IsTimeOptional = true
           },
           ReportStatus = "Corrected"
        },         
      };
      AuthorName = new NameVM()
      {
        Family = "Jones",
        Given = "Ken",
        Title = "Dr"
      };
      PdfFilePath = @"C:\GitRepository\HL7V2Examples\Pathology\NEHTA AS4700.2 2012 Examples\Result Output Example 1\FBC NEHTA Pathology Report PDF.pdf";
    }

    private bool _OkToSave;
    public bool OkToSave 
    {
      get
      {
        return _OkToSave;
      }
      set
      {
        _OkToSave = value;
        OnPropertyChanged("OkToSave");
      }
    }

    public string GetHL7Message()
    { 
      
      Oru.Patient = new Patient();
      Oru.Patient.Family = Patient.PatientName.Family;
      Oru.Patient.Given = Patient.PatientName.Given;
      Oru.Patient.Title = Patient.PatientName.Title;
      Oru.Patient.DateOfBirth = Patient.PatientDateOfBirth.Value;
      Oru.Patient.Gender = Patient._Gender;
      Oru.Patient.HomeContact = new Contact() { Value = Patient.HomePhone };
      Oru.Patient.WorkContact = new Contact() { Value = Patient.WorkPhone };
      Oru.Patient.Address = new Address()
      {
        AddressLineOne = Patient.Address.AddressLineOne,
        AddressLineTwo = Patient.Address.AddressLineTwo,
        Suburb = Patient.Address.Suburb,
        PostCode = Patient.Address.PostCode,
        State = Patient.Address.State,
        Country = Patient.Address.Country
      };
      Oru.Patient.IndigenousStatus = new IndigenousStatus()
      {
        IndigenousStatusType = Patient._IndigenousStatus
      };
      Oru.Patient.IdentifierList = new System.Collections.Generic.List<PatientIdentifier>();
      foreach(var item in PatientIdentifierList)
      {
        Oru.Patient.IdentifierList.Add(new PatientIdentifier()
        {
          Type = item._Type,
          AssigningAuthority = item.AssigningAuthority,
          Value = item.Value
        });
      }
      Oru.HospitalEncounter = new HospitalEncounter() { PatientClass = PatientClassType.OutPatient };
      Oru.Order = new PathologyOrder()
      {
        CollectionDateTime = Order.CollectionDateTime.FinalDateTimeOffSet,
        OrderedDateTime = Order.RequestedDateTime.FinalDateTimeOffSet,
        IsMyHealthRecordDisclosed = Order.IsMyHealthRecordDisclosed,
        OrderIdentifier = Order.OrderNumber,
        OrderingProvider = new Provider()
        {
          Family = Order.ProviderName.Family,
          Given = Order.ProviderName.Given,
          Title = Order.ProviderName.Title
        }
      };
      Oru.RequestList = new System.Collections.Generic.List<PathologyRequest>();
      foreach(var Item in PathologyRequestList)
      {
        Oru.RequestList.Add(new PathologyRequest()
        {
          DepartmentCode = Item.DepartmentCode,
          DocumentAuthor = new Provider()
          {
            Family = AuthorName.Family,
            Given = AuthorName.Given,
            Title = AuthorName.Title
          },
          ReportedDateTime = Item.ReportedDateTime.FinalDateTimeOffSet,
          ReportIdentifier = Item.ReportIdentifier,
          ReportName = new UniversalServiceIdentifier()
          {
            LocalCode = Item.LocalCode,
            LocalCodeDescription = Item.LocalDescription,
            LocalCodeSystemCode = Item.LocalSystemCode,
            SnomedPreferedTerm = Item.SnomedPreferredTerm,
            SnomedTermValue = Item.SnomedCode
          },
          ReportStatus = Item._ReportStatus
        });         
      }
      Oru.PDF = new PDFReport()
      {
        Filepath = @"C:\GitRepository\HL7V2Examples\Pathology\NEHTA AS4700.2 2012 Examples\Result Output Example 1\FBC NEHTA Pathology Report PDF.pdf"
      };

      return Oru.GetPathologyORUMessage();
    }

    protected override string IsValid(string PropertyName)
    {
      bool IsError = false;
      if (PropertyName == "PdfFilePath")
      {
        if (string.IsNullOrWhiteSpace(this.PdfFilePath))
        {
          AddError("PdfFilePath", $"A PDF file path must be populated.");
          IsError = true;          
        }        
      }

      if (PropertyName == "PatientDateOfBirth")
      {
        if (!this.PatientDateOfBirth.HasValue)
        {
          AddError("PatientDateOfBirth", $"Date of Birth (DOB) must be populated.");
          IsError = true;          
        }               
      }

      string Result = string.Empty;
      if (IsError)
      {
        Result = "Error Found!";
      }
      else
      {
        RemoveError(PropertyName);        
      }
      this.OkToSave = (this.CanSave && Patient.CanSave && Order.CanSave && AuthorName.CanSave);
      return Result;
    }
    
  }
}
