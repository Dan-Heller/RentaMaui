using PropertyChanged;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Renta.Models;

[AddINotifyPropertyChangedInterface]
public abstract class BaseObservableModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    }
}