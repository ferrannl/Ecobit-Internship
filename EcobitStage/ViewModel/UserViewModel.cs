using Ecobit.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            SearchCommand = new RelayCommand(Search);
            RefreshCommand = new RelayCommand(Refresh);
            NewCommand = new RelayCommand(New);
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
            Refresh();
        }

        private void Search()
        {
            var query = SearchQuery.ToLower();
            ObservableUsers.Clear();
            foreach (DataViewModel.User u in _users.Where(us => us.FirstName.ToLower().Contains(query) || us.ID.ToString() == query || us.FirstName.ToLower().Contains(query)))
            {
                ObservableUsers.Add(u);
            }
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

        private void Save()
        {
            if (SelectedUser.Validate())
            {
                Ecobit.Domain.User addUser = new Ecobit.Domain.User { ID = SelectedUser.ID, FirstName = SelectedUser.FirstName, LastName = SelectedUser.LastName, E_mail = SelectedUser.Email };
                using (var context = new EcobitDBEntities())
                {
                    var existingUser = context.User.Where(u => u.E_mail.Equals(addUser.E_mail));
                    if (existingUser.Count() != 0)
                    {
                        MessageBox.Show("Gebruiker " + SelectedUser.Email + " bestaat al.", "Bestaat al", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        context.User.Add(addUser);
                        context.SaveChanges();
                    }
                }
                Refresh();
                Cancel();
            }
        }

        private void Delete()
        {
            if (MessageBox.Show("Wil je deze gebruiker verwijderen?",
            "Verwijderen", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var context = new EcobitDBEntities())
                {
                    var user = context.User.Where(u => u.ID == SelectedUser.ID).FirstOrDefault();
                    context.User.Remove(user);
                    context.SaveChanges();
                }
                ObservableUsers.Remove(SelectedUser);
                SelectedUser = null;
            }
            else
            {
                // Do not close the window
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