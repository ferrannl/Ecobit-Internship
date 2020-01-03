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

        public DataViewModel.UserPrivilege SelectedUserPrivilege { get; set; }
        private List<DataViewModel.UserPrivilege> _userprivileges = new List<DataViewModel.UserPrivilege>();
        public ObservableCollection<DataViewModel.UserPrivilege> ObservableUserPrivileges { get; set; }

        public UserPrivilegeViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            SelectedUserPrivilege = null;
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
            SelectedUserPrivilege = new DataViewModel.UserPrivilege(2, "Outlook");
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
            if (SelectedUserPrivilege.Validate())
            {
                Ecobit.Domain.UserPrivilege addUserPrivilege = new Ecobit.Domain.UserPrivilege { User_ID = SelectedUserPrivilege.User_ID, Privilege_Name = SelectedUserPrivilege.Privilege_Name, StartDate = SelectedUserPrivilege.StartDate, EndDate = SelectedUserPrivilege.EndDate };
                using (var context = new EcobitDBEntities())
                {
                    context.UserPrivilege.Add(addUserPrivilege);
                    context.SaveChanges();
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
        }
    }
}
