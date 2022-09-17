using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    [QueryProperty(nameof(ImageSourceString), "url")]
    public class PhotoDisplayPageViewModel : BaseViewModel
    {
        public string _imageSource { get; set; }
        public string _ImageSource { get; set; }
        public String ImageSourceString
        {
            get => _ImageSource;
            set
            {
                _ImageSource = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged(nameof(_ImageSource));
            }
        }

        public void ActivatePropertyChange()
        {
            _imageSource = JsonConvert.DeserializeObject<string>(_ImageSource);
            OnPropertyChanged(nameof(_imageSource));
        }
    }
}