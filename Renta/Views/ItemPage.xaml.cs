using Renta.ViewModels;
namespace Renta;

public partial class ItemPage : ContentPage
{
	public bool HeartButtonClicked { get; set; }

	public ItemPage()
	{
		BindingContext = new ItemPageViewModel();
		InitializeComponent();

		SetHeartButton();
		
	}

	private  void SetHeartButton()
    {
		if((BindingContext as ItemPageViewModel).ItemLiked == false)
        {
			HeartButton.IconImageSource = "hollowhearticon.png";
		}
        else
        {
			HeartButton.IconImageSource = "fullhearticon.png";
		}
    }

	

    private void HeartButton_Clicked(object sender, EventArgs e)
    {
		(BindingContext as ItemPageViewModel).ItemLiked = !(BindingContext as ItemPageViewModel).ItemLiked;
		SetHeartButton();

	}
}