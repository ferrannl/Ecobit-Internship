using Ecobit.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EcobitStage.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        public ICommand RefreshCommand { get; set; }
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
            ObservableUsers = new ObservableCollection<User>();
            RefreshCommand = new RelayCommand(Refresh);
            SearchCommand = new RelayCommand(Search);
            EditCommand = new RelayCommand(Edit);
            NewCommand = new RelayCommand(New);
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
            Refresh();
        }

        private void Cancel()
        {
            CommonServiceLocator.ServiceLocator.Current.GetInstance<MainViewModel>().OpenUserListView();
        }
        private void Refresh()
        {

            _users.Clear();
            ObservableUsers.Clear();
            var list = JsonConvert.DeserializeObject<List<CustomerDTO>>(text);

            foreach (CustomerDTO c in list)
            {
                _customers.Add(new Customer(c));
                ObservableCustomers.Add(new Customer(c));

            }
        }
    }
}
