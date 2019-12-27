
using EcobitStage.Offline;
using EcobitStage.ViewModel.DataViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EcobitStage.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private string _searchQuery;
        public string SearchQuery
        {
            get
            {
                return _searchQuery;
            }
            set
            {
                _searchQuery = value;
                RaisePropertyChanged("SearchQuery");
            }
        }
        public User SelectedUser { get; set; }
        private List<User> _users = new List<User>();
        public ObservableCollection<User> ObservableUsers { get; set; }

        public UserViewModel()
        {
            Initialize();
            Refresh();
        }
        private void Initialize()
        {
            SelectedUser = null;
            ObservableUsers = new ObservableCollection<User>();
            RefreshCommand = new RelayCommand(Refresh);
            DeleteCommand = new RelayCommand(Delete);
            //SearchCommand = new RelayCommand(Search);
            //EditCommand = new RelayCommand(Edit);
            //NewCommand = new RelayCommand(New);
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }
        private void Save()
        {
            if (ConnectionCheck.Online)
            {
                if (SelectedUser.Validate())
                {
                    var list = new List<UserDTO>();
                    list.Add((UserDTO)SelectedUser.ConvertToDTO());
                    Refresh();
                    Cancel();
                }
            }
            else
            {
                MessageBox.Show(ConnectionCheck.errorMsg);
            }

        }
        private void Cancel()
        {
            CommonServiceLocator.ServiceLocator.Current.GetInstance<MainViewModel>().OpenUserListView();
        }

        private void Delete()
        {
            if (ConnectionCheck.Online)
            {
                ObservableUsers.Remove(SelectedUser);
                SelectedUser = null;
            }
            else
            {
                System.Windows.MessageBox.Show(ConnectionCheck.errorMsg);
            }
        }

        private void Refresh()
        {
            _users.Clear();
            ObservableUsers.Clear();
            using (var context = new EcobitDBEntities())
            {
                List<User> list = new List<User>(context.User.ToList());
                foreach (User u in list)
                {
                    _users.Add(u);
                    ObservableUsers.Add(u);
                }
            }
        }
    }
}
