using Xamarin.Forms;

namespace AppListView
{
    public partial class MainPage : ContentPage, IMessage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel() { Message = this};
        }
    }
}
