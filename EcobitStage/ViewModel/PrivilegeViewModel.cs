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
    public class PrivilegeViewModel : ViewModelBase
    {
        #region Commands

        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private string _searchQuery;

        #endregion Commands

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

        public DataViewModel.Privilege SelectedPrivilege { get; set; }
        private List<DataViewModel.Privilege> _privileges = new List<DataViewModel.Privilege>();
        public ObservableCollection<DataViewModel.Privilege> ObservablePrivileges { get; set; }

        public PrivilegeViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            SelectedPrivilege = null;
            ObservablePrivileges = new ObservableCollection<DataViewModel.Privilege>();
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
            ObservablePrivileges.Clear();
            foreach (DataViewModel.Privilege p in _privileges.Where(up => up.Name.ToLower().Contains(query) || up.Name.ToString() == query || up.Name.ToLower().Contains(query)))
            {
                ObservablePrivileges.Add(p);
            }
        }

        private void New()
        {
            SelectedPrivilege = new DataViewModel.Privilege();
            Edit();
        }

        private void Edit()
        {
            CommonServiceLocator.ServiceLocator.Current.GetInstance<MainViewModel>().OpenPrivilegeEditView();
        }

        private void Cancel()
        {
            CommonServiceLocator.ServiceLocator.Current.GetInstance<MainViewModel>().OpenPrivilegeListView();
        }

        private void Save()
        {
            if (SelectedPrivilege.Validate())
            {
                bool saved = true;
                Ecobit.Domain.Privilege addPrivilege = new Ecobit.Domain.Privilege { Name = SelectedPrivilege.Name };
                using (var context = new EcobitDBEntities())
                {
                    List<Ecobit.Domain.Privilege> list = new List<Ecobit.Domain.Privilege>(context.Privilege.ToList());
                    foreach (Ecobit.Domain.Privilege p in list)
                    {
                        if (p.Name.ToLower() == SelectedPrivilege.Name.ToLower())
                        {
                            MessageBox.Show(SelectedPrivilege.Name + " bestaat al.",
                                "Bestaat al", MessageBoxButton.OK);
                            saved = false;
                        }
                    }
                    if (saved == true)
                    {
                        context.Privilege.Add(addPrivilege);
                        context.SaveChanges();
                    }
                    Refresh();
                    Cancel();
                }
            }
        }

        private void Delete()
        {
            if (MessageBox.Show("Wil je " + SelectedPrivilege.Name + " verwijderen?",
            "Verwijderen", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var context = new EcobitDBEntities())
                {
                    var privilege = context.Privilege.Where(p => p.Name == SelectedPrivilege.Name).FirstOrDefault();
                    context.Privilege.Remove(privilege);
                    context.SaveChanges();
                }
                ObservablePrivileges.Remove(SelectedPrivilege);
                SelectedPrivilege = null;
            }
            else
            {
                // Do not close the window
            }
        }

        private void Refresh()
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
    }
}