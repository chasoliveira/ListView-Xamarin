using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppListView
{
    public class NotifyPropertyChange : INotifyPropertyChanged
    {
        #region Notify
        public IMessage Message { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
    public class Item : NotifyPropertyChange
    {
        string _title;
        string _subtitle;
        public string Title { get { return _title; } set { _title = value; RaisePropertyChanged(nameof(Title)); } }
        public string SubTitle { get { return _subtitle; } set { _subtitle = value; RaisePropertyChanged(nameof(SubTitle)); } }
    }

    public class MainViewModel : NotifyPropertyChange
    {
        public event EventHandler<string> ItemHandled;
        string _title, _subTitle;
        Item _currentItem;
        ObservableCollection<Item> _list01;
        ObservableCollection<Item> _list02;

        ICommand _addToSecondListCommand;
        ICommand _addToFirstListCommand;
        ICommand _removeFormBothListCommand;

        public MainViewModel()
        {
            List01 = new ObservableCollection<Item>();
            List02 = new ObservableCollection<Item>();
            CurrentItem = new Item();
        }
        public string Title { get { return _title; } set { _title = value; RaisePropertyChanged(nameof(Title)); } }
        public string SubTitle { get { return _subTitle; } set { _subTitle = value; RaisePropertyChanged(nameof(SubTitle)); } }
        public Item CurrentItem { get { return _currentItem; } set { _currentItem = value; RaisePropertyChanged(nameof(CurrentItem)); } }
        public ObservableCollection<Item> List01 { get { return _list01; } set { _list01 = value; RaisePropertyChanged(nameof(List01)); } }
        public ObservableCollection<Item> List02 { get { return _list02; } set { _list02 = value; RaisePropertyChanged(nameof(List02)); } }

        public ICommand AddToFirstListCommand
        {
            get
            {
                return _addToFirstListCommand ?? (_addToFirstListCommand = new Command(() =>
                {
                    if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(SubTitle))
                        return;
                    List01.Add(new Item { Title = Title, SubTitle = SubTitle });
                    Title = ""; SubTitle = "";
                    ItemHandled?.Invoke(this, "Item added Successfuly");
                }));
            }
        }

        public ICommand RemoveFromFirtListCommand
        {
            get
            {
                return _removeFormBothListCommand ?? (_removeFormBothListCommand = new Command(async () =>
                {
                    List01.Remove(CurrentItem);
                    await Message.DisplayAlert("List View", "Item Remove Successfully.", "OK");
                }));
            }
        }
        public ICommand AddToSecondListCommand
        {
            get
            {
                return _addToSecondListCommand ?? (_addToSecondListCommand = new Command(async () =>
                {
                    if (CurrentItem != null)
                    {
                        List02.Add(CurrentItem);
                        CurrentItem = null;
                        await Message.DisplayAlert("List View", "Item Added to Second List Successfully.", "OK");
                    }
                }));
            }
        }
    }
}
