using Renta.enums;
using Renta.ViewModels;

namespace Renta.Controls;

public partial class RentingPageTabsControl : ContentView
{
    public ETransactionStatus SelectedTab;

    public RentingPageTabsControl()
    {
        InitializeComponent();
        BindingContext = this;
        RequestsLabel.TextColor = Color.FromArgb("000000");
    }


    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        RequestsLabel.TextColor = Color.FromArgb("808080");
        ApprovedLabel.TextColor = Color.FromArgb("808080");
        ActiveLabel.TextColor = Color.FromArgb("808080");
        HistoryLabel.TextColor = Color.FromArgb("808080");

        (sender as Label).TextColor = Color.FromArgb("000000");
    }

    private ETransactionStatus SwitchClassIdToEnum(string classId)
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
}