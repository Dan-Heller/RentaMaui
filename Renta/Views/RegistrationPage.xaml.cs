using Renta.ViewModels;

namespace Renta;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage(RegistrationPageViewModel registrationPageViewModel)
    {
        BindingContext = registrationPageViewModel;
        InitializeComponent();
    }
}