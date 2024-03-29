using Renta.ViewModels;
using System.Windows.Input;

namespace Renta.Controls;

public partial class HeaderControl : ContentView
{
    //public static readonly BindableProperty SearchCommandProperty =
    //    BindableProperty.Create(
    //        nameof(SearchCommand),
    //        typeof(ICommand),
    //        typeof(HeaderControl),
    //        null);

    //public static readonly BindableProperty TextToSearchProperty =
    //    BindableProperty.Create(
    //        nameof(TextToSearch),
    //        typeof(string),
    //        typeof(HeaderControl),
    //        string.Empty);

    //public static readonly BindableProperty ShowSearchCategoriesProperty =
    //    BindableProperty.Create(
    //        nameof(ShowSearchCategories),
    //        typeof(bool),
    //        typeof(HeaderControl),
    //        true);

    //public bool ShowSearchCategories
    //{
    //    get { return (bool)GetValue(ShowSearchCategoriesProperty); }
    //    set { SetValue(ShowSearchCategoriesProperty, value); }
    //}

    public static readonly BindableProperty ShowProfileButtonProperty =
        BindableProperty.Create(
            nameof(ShowProfileButtonProperty),
            typeof(bool),
            typeof(HeaderControl),
            true);

    public bool ShowProfileButton
    {
        get { return (bool)GetValue(ShowProfileButtonProperty); }
        set { SetValue(ShowProfileButtonProperty, value); }
    }

    //public ICommand SearchCommand
    //{
    //    get { return (ICommand)GetValue(SearchCommandProperty); }
    //    set { SetValue(SearchCommandProperty, value); }
    //}

    //public string TextToSearch
    //{
    //    get { return (string)GetValue(TextToSearchProperty); }
    //    set { SetValue(TextToSearchProperty, value); }
    //}

    public HeaderControl()
    {
        InitializeComponent();
    }

    private async void ProfileButton_Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(ProfilePage)}");
    }

    //private async void CreditButton_Tapped(object sender, EventArgs e)
    //{
    //    await Shell.Current.GoToAsync($"{nameof(CreditsPage)}");
    //}

    private async void AddITtemButton_Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(AddItemPage)}");
    }

    private async void NotificationsButton_Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(NotificationsPage)}");
    }

 

}