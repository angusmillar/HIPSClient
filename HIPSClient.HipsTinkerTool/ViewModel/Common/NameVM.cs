namespace HIPSClient.HipsTinkerTool.ViewModel.Common
{
  public class NameVM : BaseValidationVM
  {
    private string _Family;
    public string Family
    {
      get
      {
        return _Family;
      }
      set
      {
        _Family = value;
        OnPropertyChanged("Family");
        OnPropertyChanged("NameFormated");
      }
    }

    private string _Given;
    public string Given
    {
      get
      {
        return _Given;
      }
      set
      {
        _Given = value;
        OnPropertyChanged("Given");
        OnPropertyChanged("NameFormated");
      }
    }

    private string _Title;
    public string Title
    {
      get
      {
        return _Title;
      }
      set
      {
        _Title = value;
        OnPropertyChanged("Title");
        OnPropertyChanged("NameFormated");
      }
    }

    public string NameFormated
    {
      get
      {
        return $"{this.Family}, {this.Given}, {this.Title}";
      }
    }

    protected override string IsValid(string PropertyName)
    {      
      if (PropertyName == "Family")
      {
        if (string.IsNullOrWhiteSpace(this.Family))
        {
          AddError("Family", $"Family name must be populated.");
          return "Error Found!";
        }
        RemoveError("Family");
      }      
      return string.Empty;
    }
  }
}
