using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIPSClient.Tool.ViewModel
{
  public abstract class BaseVM : INotifyPropertyChanged
  {    
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChange(string PropertyName)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null)
      {
        handler(this, new PropertyChangedEventArgs(PropertyName));
      }
    }
  }
}
