using Renta.enums;
using Renta.ViewModels;
namespace Renta.Controls;

public partial class RentingPageTabsControl : ContentView
{
	//private RentingPageViewModel viewModel;
	public ETransactionStatus SelectedTab;

	public RentingPageTabsControl() 
	{
		InitializeComponent();
		BindingContext = this;
		//viewModel = rentingPageViewModel;
		RequestsLabel.TextColor = Color.FromArgb("000000");
		

	}

	

	private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
		RequestsLabel.TextColor = Color.FromArgb("808080");
		ApprovedLabel.TextColor = Color.FromArgb("808080");
		ActiveLabel.TextColor = Color.FromArgb("808080");
		HistoryLabel.TextColor = Color.FromArgb("808080");

		(sender as Label).TextColor = Color.FromArgb("000000");

		//change the selected tab in renting view model 
		//viewModel.SelectedTabInTabsController = switchClassIdToEnum((sender as Label).ClassId);
	}

	private ETransactionStatus switchClassIdToEnum(string ClassId)
    {
		ETransactionStatus status = ETransactionStatus.Pending;

        switch (ClassId)
        {
			case "Pending":
				status = ETransactionStatus.Pending;
				break;
			case "Approved":
				status = ETransactionStatus.Approved;
				break;
			case "Active":
				status = ETransactionStatus.Active;
				break;
			case "History":
				status = ETransactionStatus.Archived;
				break;
		}

		return status;
	}

}