using MvvmHelpers;
using Newtonsoft.Json;
using Renta.Models;
using Renta.Services;

namespace Renta.ViewModels
{
    [QueryProperty(nameof(MyItemString), "item")]
    public class MyItemPageViewModel : ObservableObject
    {
        public ItemViewModel _myItemViewModel { get; set; }
        public ReviewsService _reviewService { get; set; }

        private UserService _userService { get; set; }

        public ObservableRangeCollection<ItemReview> ItemReviewsCollection { get; private set; } =
            new ObservableRangeCollection<ItemReview>();

        private string _MyItemString;

        public String MyItemString
        {
            get => _MyItemString;
            set { _MyItemString = Uri.UnescapeDataString(value ?? string.Empty); }
        }

        public MyItemPageViewModel(ReviewsService reviewsService, UserService userService)
        {
            _reviewService = reviewsService;
            _userService = userService;
        }

        public async Task FetchItemReviews()
        {
            ItemReviewsCollection.ReplaceRange(await _reviewService.GetReviewsOnItem(_myItemViewModel.Id));
        }

        public void deserializeString()
        {
            var Item = JsonConvert.DeserializeObject<Item>(_MyItemString);
            convertItemToViewModel(Item);
        }

        private void convertItemToViewModel(Item item)
        {
            _myItemViewModel = new ItemViewModel(item, _userService);
            OnPropertyChanged(nameof(_myItemViewModel));
        }

        public Command ProfileLink_Tapped
            => new Command(async () => await Shell.Current.GoToAsync($"{nameof(ProfilePage)}"));

        public Command EditButtonClicked
            => new Command(async () => await EditButton_Tapped());

        private async Task EditButton_Tapped()
        {
            var jsonStr = JsonConvert.SerializeObject(_myItemViewModel.Item);
            await Shell.Current.GoToAsync($"{nameof(EditItemPage)}?item={jsonStr}");
        }

        public Command NextImage_Clicked
            => new Command(() => ShowNextImage());

        public void ShowNextImage()
        {
            _myItemViewModel.CurrentImageIndex++;
            OnPropertyChanged(nameof(_myItemViewModel));
        }

        public Command PreviousImage_Clicked
            => new Command(() => ShowPreviousImage());

        public void ShowPreviousImage()
        {
            _myItemViewModel.CurrentImageIndex = _myItemViewModel.CurrentImageIndex == 0
                ? _myItemViewModel.ImagesUrls.Count - 1
                : _myItemViewModel.CurrentImageIndex - 1;
            OnPropertyChanged(nameof(_myItemViewModel));
        }
    }
}