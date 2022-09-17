namespace Renta.ViewModels
{
    public class CategoriesPageViewModel
    {
        public Command CategoryOptionTappedGoBackToSearchPage
            => new Command(async () => await Shell.Current.GoToAsync(".."));
    }
}