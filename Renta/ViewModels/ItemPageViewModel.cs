using System.Collections.Specialized;
using System.Windows.Input;
using Newtonsoft.Json;
using Renta.Dto_s;
using Renta.Models;
using Renta.Services;
using XCalendar.Core.Enums;
using XCalendar.Core.Models;

namespace Renta.ViewModels
{
    [QueryProperty(nameof(ItemString), "item")]
    public class ItemPageViewModel : BaseViewModel
    {
        public bool ItemLiked;
        public ItemViewModel _ItemViewModel { get; set; }

        private UserService _userService { get; set; }
        private TransactionService _transactionService { get; set; }
        private ReviewsService _reviewService { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime Today { get; set; } = DateTime.Now;

        public ObservableRangeCollection<DateTime> datesCollection { get; set; }

        public ObservableRangeCollection<ItemReview> ItemReviewsCollection { get; private set; } =
            new ObservableRangeCollection<ItemReview>();

        private string _ItemString;

        public String ItemString
        {
            get => _ItemString;
            set { _ItemString = Uri.UnescapeDataString(value ?? string.Empty); }
        }

        public void deserializeString()
        {
            var item = JsonConvert.DeserializeObject<Item>(_ItemString);
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
            Console.WriteLine(item);
            Console.WriteLine(_ItemString);
            convertItemToViewModel(item);
            InitializeCalendar();
            ItemLiked = _ItemViewModel.ItemLiked;
        }

        public async Task FetchItemReviews()
        {
            ItemReviewsCollection.ReplaceRange(await _reviewService.GetReviewsOnItem(_ItemViewModel.Id));
        }

        private void convertItemToViewModel(Item item)
        {
            _ItemViewModel = new ItemViewModel(item, _userService);
            OnPropertyChanged(nameof(_ItemViewModel));
        }

        public ItemPageViewModel(UserService userService, TransactionService transactionService,
            ReviewsService reviewsService)
        {
            _userService = userService;
            _transactionService = transactionService;
            _reviewService = reviewsService;

            NavigateCalendarCommand = new Command<int>(NavigateCalendar);
            ChangeDateSelectionCommand = new Command<DateTime>(ChangeDateSelection);
        }

        public void InitializeCalendar()
        {
            foreach (var unAvailableDate in _ItemViewModel.UnAvailableDates)
            {
                for (var date = unAvailableDate.StartDate;
                     date <= unAvailableDate.EndDate;
                     date = date.Value.AddDays(1))
                {
                    Events.Add(new Event()
                    {
                        Title = "Unavailable",
                        Description = "Unavailable",
                        Color = Colors[0],
                        DateTime = date ?? DateTime.Now,
                    });
                }
            }

            EventCalendar.SelectedDates.CollectionChanged += SelectedDates_CollectionChanged;
            EventCalendar.DaysUpdated += EventCalendar_DaysUpdated;
            foreach (var Day in EventCalendar.Days)
            {
                Day.Events.ReplaceRange(Events.Where(x => x.DateTime.Date == Day.DateTime.Date));
            }
        }


        public Command ProfileLink_Tapped
            => new Command(async () =>
                await Shell.Current.GoToAsync($"{nameof(OtherUserProfilePage)}?OwnerId={_ItemViewModel.OwnerId}"));

        public Command SendRequestButton_Clicked
            => new Command(async () => await sendItemRequest());

        public async Task sendItemRequest()
        {
            //check if user has enough coins.           
            bool enoughCoins =
                await _userService.GetIfBalanceIsValidForRent((int)_ItemViewModel.PricePerDay, datesCollection.Count);

            if (!enoughCoins)
            {
                await Application.Current.MainPage.DisplayAlert(" ", "You don't have enought RentA coins", "close");
                return;
            }

            //check if dates overlap taken dates
            bool IsOverlap = _ItemViewModel.CheckIfRequestedDatesOverlapUnavailableDates(datesCollection[0],
                datesCollection[datesCollection.Count - 1]);

            if (!IsOverlap)
            {
                var transactionDto = createTransaction();
                await _transactionService.CreateTransaction(transactionDto);
                await Application.Current.MainPage.DisplayAlert(" ", "Request sent, you can watch it on Rental Page.",
                    "close");
            }
            else
            {
                //show messege to ui ...  later convert to imessege 
                await Application.Current.MainPage.DisplayAlert("Failed", "Dates Unavailable", "close");
            }
        }

        #region Properties

        public Calendar<EventDay> EventCalendar { get; set; } = new Calendar<EventDay>()
        {
            SelectedDates = new ObservableRangeCollection<DateTime>(),
            SelectionAction = SelectionAction.Modify,
            SelectionType = SelectionType.Single,
            NavigationLowerBound = DateTime.Now,
        };

        public static readonly Random Random = new Random();

        public List<Color> Colors { get; } = new List<Color>()
        {
            Microsoft.Maui.Graphics.Colors.Red,
            Microsoft.Maui.Graphics.Colors.Blue,
        };

        public ObservableRangeCollection<Event> Events { get; } = new ObservableRangeCollection<Event>();
        // {
        //     new Event() { Title = "Occupied", Description = "item is unavailable" },
        // };

        public ObservableRangeCollection<Event> SelectedEvents { get; } = new ObservableRangeCollection<Event>();

        #endregion


        #region Commands

        public ICommand NavigateCalendarCommand { get; set; }
        public ICommand ChangeDateSelectionCommand { get; set; }

        #endregion

        #region Methods

        private void EventCalendar_DaysUpdated(object sender, EventArgs e)
        {
            foreach (var Day in EventCalendar.Days)
            {
                Day.Events.ReplaceRange(Events.Where(x => x.DateTime.Date == Day.DateTime.Date));
            }
        }

        private void SelectedDates_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SelectedEvents.ReplaceRange(Events
                .Where(x => EventCalendar.SelectedDates.Any(y => x.DateTime.Date == y.Date))
                .OrderByDescending(x => x.DateTime));

            datesCollection = EventCalendar.SelectedDates;
        }

        public void NavigateCalendar(int Amount)
        {
            EventCalendar?.NavigateCalendar(Amount);
        }

        public void ChangeDateSelection(DateTime DateTime)
        {
            EventCalendar?.ChangeDateSelection(DateTime);
        }

        #endregion

        private CreateTransactionDto createTransaction()
        {
            CreateTransactionDto transaction = new CreateTransactionDto();
            transaction.ItemSeeker = _userService.LoggedInUser.Id;
            transaction.ItemOwner = _ItemViewModel.OwnerId;
            transaction.ItemId = _ItemViewModel.Id;
            transaction.StartDate = datesCollection[0].ToLocalTime();
            transaction.EndDate = datesCollection[datesCollection.Count - 1].ToLocalTime();
            transaction.CreatedAt = DateTime.Now;
            return transaction;
        }


        public Command NextImage_Clicked
            => new Command(() => ShowNextImage());

        public void ShowNextImage()
        {
            _ItemViewModel.CurrentImageIndex++;
            OnPropertyChanged(nameof(_ItemViewModel));
        }

        public Command PreviousImage_Clicked
            => new Command(() => ShowPreviousImage());

        public void ShowPreviousImage()
        {
            _ItemViewModel.CurrentImageIndex = _ItemViewModel.CurrentImageIndex == 0
                ? _ItemViewModel.ImagesUrls.Count - 1
                : _ItemViewModel.CurrentImageIndex - 1;
            OnPropertyChanged(nameof(_ItemViewModel));
        }
    }
}