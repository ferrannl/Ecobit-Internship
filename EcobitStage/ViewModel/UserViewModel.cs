using Ecobit.Domain;
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
        public DataViewModel.User SelectedUser { get; set; }
        private List<DataViewModel.User> _users = new List<DataViewModel.User>();
        public ObservableCollection<DataViewModel.User> ObservableUsers { get; set; }

        public UserViewModel()
        {
            Initialize();
        }
        private void Initialize()
        {
            SelectedUser = null;
            ObservableUsers = new ObservableCollection<DataViewModel.User>();
            DeleteCommand = new RelayCommand(Delete);
            //SearchCommand = new RelayCommand(Search);
            //EditCommand = new RelayCommand(Edit);
            NewCommand = new RelayCommand(New);
            //SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
            Refresh();
        }

        private void New()
        {
            SelectedUser = new DataViewModel.User(-1);
            Edit();
        }

        private void Edit()
        {
            CommonServiceLocator.ServiceLocator.Current.GetInstance<MainViewModel>().OpenUserEditView();
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
                List<Ecobit.Domain.User> list = new List<Ecobit.Domain.User>(context.User.ToList());
                foreach (Ecobit.Domain.User u in list)
                {
                    DataViewModel.User newU = new DataViewModel.User(u.ID, u.FirstName, u.LastName, u.E_mail);
                    _users.Add(newU);
                    ObservableUsers.Add(newU);
                }
            }
        }
    }
}
