using Renta.enums;
using Renta.ViewModels;

namespace Renta;

public partial class RentingPage : ContentPage
{
    public RentingPage(RentingPageViewModel rentingPageViewModel)
    {
        BindingContext = rentingPageViewModel;
        InitializeComponent();

        RequestsLabel.TextColor = Color.FromArgb("000000");
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        if ((sender as Label).TextColor != Color.FromArgb("000000"))
        {
            RequestsLabel.TextColor = Color.FromArgb("808080");
            ApprovedLabel.TextColor = Color.FromArgb("808080");
            ActiveLabel.TextColor = Color.FromArgb("808080");
            HistoryLabel.TextColor = Color.FromArgb("808080");

            (sender as Label).TextColor = Color.FromArgb("000000");

            //change the selected tab in renting view model 
            (BindingContext as RentingPageViewModel).selectedStatusInTabsController =
                switchClassIdToEnum((sender as Label).ClassId);
            await (BindingContext as RentingPageViewModel).FetchTransactionByStatus();
        }
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

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as RentingPageViewModel).FetchTransactionByStatus();
    }
}