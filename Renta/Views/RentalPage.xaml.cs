using Renta.enums;
using Renta.ViewModels;

namespace Renta;

public partial class RentalPage : ContentPage
{
    public RentalPage(RentalPageViewModel rentalPageViewModel)
    {
        BindingContext = rentalPageViewModel;
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
            (BindingContext as RentalPageViewModel).selectedStatusInTabsController =
                switchClassIdToEnum((sender as Label).ClassId);
            await (BindingContext as RentalPageViewModel).FetchTransactionByStatus();
        }
    }

    private ETransactionStatus switchClassIdToEnum(string classId)
    {
        ETransactionStatus status = ETransactionStatus.Pending;

        switch (classId)
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
        await (BindingContext as RentalPageViewModel).FetchTransactionByStatus();
    }
}