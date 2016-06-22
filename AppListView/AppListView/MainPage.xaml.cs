using Xamarin.Forms;

namespace AppListView
{
    public partial class MainPage : ContentPage, IMessage
    {
        MainViewModel _mainViewModel;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = _mainViewModel = new MainViewModel() { Message = this };
            _mainViewModel.ItemHandled += (s, message) => DisplayAlert("List View", message, "Ok");
        }
    }
}
