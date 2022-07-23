using Renta.ViewModels;

namespace Renta.Controls;

public partial class RentalTransactionControl : ContentView
{
	
	

	public RentalTransactionControl()
	{
		
		InitializeComponent();
		//var status = (BindingContext as TransactionViewModel).Item.Name;
		//if (status == enums.ETransactionStatus.Pending)
		//      {
		//          AcceptButton.IsVisible = false;
		//      }
		//if (MainFrame.ClassId == enums.ETransactionStatus.Pending.ToString())
		//{
		//	AcceptButton.IsVisible = false;
		//}
		//var x = (BindingContext as TransactionViewModel).Status;
	}

	private async void ItemImage_Tapped(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync($"{nameof(MyItemPage)}");
	}

	private async void OtherUserProfile_Tapped(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync($"{nameof(OtherUserProfilePage)}");
	}





}