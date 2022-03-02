namespace Renta;

public partial class MainPage : TabbedPage
{
	

	public MainPage()
	{
		InitializeComponent();

		new NavigationPage(new FeedPage());
 	}

}

