using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    //: INotifyPropertyChanged
    public class FeedPageViewModel
    {
        public INavigation Navigation { get; set; }
        public ICommand ProfileCommand { get; private set; }
        public ICommand CreditsCommand { get; private set; }

        public FeedPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
           ProfileCommand = new Command(async () => await GoToUserPage());
           CreditsCommand = new Command(async () => await GotoCreditsPage());
        }

        public async Task GoToUserPage()
        {
            await Navigation.PushAsync(new ProfilePage());
        }

        public async Task GotoCreditsPage()
        {
            await Navigation.PushAsync(new CreditsPage());
        }
    }
}