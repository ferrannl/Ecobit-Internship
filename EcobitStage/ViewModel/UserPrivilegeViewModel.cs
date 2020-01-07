using Ecobit.Domain;
using EcobitStage.DataTransfer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EcobitStage.ViewModel
{
    public class UserPrivilegeViewModel : ViewModelBase
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

        private List<DataViewModel.User> _users = new List<DataViewModel.User>();
        private List<DataViewModel.Privilege> _privileges = new List<DataViewModel.Privilege>();
        private List<DataViewModel.UserPrivilege> _userprivileges = new List<DataViewModel.UserPrivilege>();

        public DataViewModel.User SelectedUser { get; set; }
        public DataViewModel.Privilege SelectedPrivilege { get; set; }
        public DataViewModel.UserPrivilege SelectedUserPrivilege { get; set; }

        public ObservableCollection<DataViewModel.User> ObservableUsers { get; set; }
        public ObservableCollection<DataViewModel.Privilege> ObservablePrivileges { get; set; }
        public ObservableCollection<DataViewModel.UserPrivilege> ObservableUserPrivileges { get; set; }

        public UserPrivilegeViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            SelectedUser = null;
            SelectedPrivilege = null;
            SelectedUserPrivilege = null;
            ObservableUsers = new ObservableCollection<DataViewModel.User>();
            ObservablePrivileges = new ObservableCollection<DataViewModel.Privilege>();
            ObservableUserPrivileges = new ObservableCollection<DataViewModel.UserPrivilege>();

            DeleteCommand = new RelayCommand(Delete);
            //SearchCommand = new RelayCommand(Search);
            //EditCommand = new RelayCommand(Edit);
            NewCommand = new RelayCommand(New);
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
            Refresh();
        }

        private void New()
        {
            SelectedUser = new DataViewModel.User(-1);
            SelectedPrivilege = new DataViewModel.Privilege();
            SelectedUserPrivilege = new DataViewModel.UserPrivilege();
            Edit();
        }

        private void Edit()
        {
            CommonServiceLocator.ServiceLocator.Current.GetInstance<MainViewModel>().OpenUserPrivilegeEditView();
            Refresh();
        }

        private void Cancel()
        {
            CommonServiceLocator.ServiceLocator.Current.GetInstance<MainViewModel>().OpenUserPrivilegeListView();
        }

        private void Save()
        {
            bool saved = true;
            if (SelectedPrivilege.Validate() && SelectedUser.Validate() && SelectedUserPrivilege.Validate())
            {
                Ecobit.Domain.UserPrivilege addUserPrivilege = new Ecobit.Domain.UserPrivilege { User_ID = SelectedUser.ID, Privilege_Name = SelectedPrivilege.Name, StartDate = SelectedUserPrivilege.StartDate, EndDate = SelectedUserPrivilege.EndDate };
                using (var context = new EcobitDBEntities())
                {
                    List<Ecobit.Domain.UserPrivilege> list = new List<Ecobit.Domain.UserPrivilege>(context.UserPrivilege.ToList());

                    foreach (Ecobit.Domain.UserPrivilege up in list)
                    {
                        if (up.Privilege_Name.ToLower() == SelectedPrivilege.Name.ToLower() && up.User_ID == SelectedUser.ID)
                        {
                            MessageBox.Show("Combinatie " + SelectedUser.FirstName + " - " + SelectedPrivilege.Name + " bestaat al.",
                                "Combinatie bestaat al", MessageBoxButton.OK);
                            saved = false;
                        }
                    }
                    if (saved == true)
                    {
                        context.UserPrivilege.Add(addUserPrivilege);
                        context.SaveChanges();
                    }
                }
                Refresh();
                Cancel();
            }
        }

        private void Delete()
        {
            if (MessageBox.Show("Wil je deze gebruikertoegang verwijderen?",
            "Verwijderen", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var context = new EcobitDBEntities())
                {
                    var userprivilege = context.UserPrivilege.Where(up => up.User_ID == SelectedUserPrivilege.User_ID && up.Privilege_Name == SelectedUserPrivilege.Privilege_Name).FirstOrDefault();
                    context.UserPrivilege.Remove(userprivilege);
                    context.SaveChanges();
                }
                ObservableUserPrivileges.Remove(SelectedUserPrivilege);
                SelectedUserPrivilege = null;
            }
            else
            {
                // Do not close the window
            }
        }

        public string GetFullnameByID(int id)
        {
            string fullname = null;
            using (var context = new EcobitDBEntities())
            {
                List<Ecobit.Domain.UserPrivilege> list = new List<Ecobit.Domain.UserPrivilege>(context.UserPrivilege.ToList());

                var user = context.User.FirstOrDefault(i => i.ID == id);
                fullname = user.FirstName + " " + user.LastName;

            }
            if (fullname != null)
            {
                return fullname;
            }
            else
            {
                return "geen_naam";
            }
        }

        private void Refresh()
        {
            _userprivileges.Clear();
            ObservableUserPrivileges.Clear();
            using (var context = new EcobitDBEntities())
            {
                List<Ecobit.Domain.UserPrivilege> list = new List<Ecobit.Domain.UserPrivilege>(context.UserPrivilege.ToList());
                foreach (Ecobit.Domain.UserPrivilege up in list)
                {
                    DataViewModel.UserPrivilege newUP = new DataViewModel.UserPrivilege(up.User_ID, up.Privilege_Name, up.StartDate, up.EndDate);
                    newUP.Fullname = GetFullnameByID(newUP.User_ID);
                    _userprivileges.Add(newUP);
                    ObservableUserPrivileges.Add(newUP);
                }
            }
            RefreshPrivileges();
            RefreshUsers();
        }
        private void RefreshPrivileges()

        {
            _privileges.Clear();
            ObservablePrivileges.Clear();
            using (var context = new EcobitDBEntities())
            {
                List<Ecobit.Domain.Privilege> list = new List<Ecobit.Domain.Privilege>(context.Privilege.ToList());
                foreach (Ecobit.Domain.Privilege p in list)
                {
                    DataViewModel.Privilege newP = new DataViewModel.Privilege(p.Name);
                    _privileges.Add(newP);
                    ObservablePrivileges.Add(newP);
                }
            }
        }

        private void RefreshUsers()
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
