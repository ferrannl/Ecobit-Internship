using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace EcobitStage.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //public static string baseUrl = "http://localhost:54109/";

        #region properties & fields

        private Uri _currentPage;

        [System.ComponentModel.Bindable(true)]
        //Get Set for current showing page
        public Uri CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                RaisePropertyChanged("CurrentPage");
            }
        }

        public MessageBoxManager MessageBoxManager { get; set; }
        public ViewUriFactory Factory { get; set; }
        public AccountViewModel Account { get; set; }
        public string TopText { get; set; }

        //Menubar Commands and CanExecutes.
        public ICommand UserPrivilegeList { get; set; }

        public ICommand PrivilegeList { get; set; }
        public ICommand UserList { get; set; }
        public ICommand Logout { get; set; }

        #endregion properties & fields

        //Initializing MainViewModel
        public MainViewModel()
        {
            MessageBoxManager = new MessageBoxManager();
            MessageBoxManager.OK = "OK";
            MessageBoxManager.Yes = "Ja";
            MessageBoxManager.No = "Nee";
            MessageBoxManager.Cancel = "Annuleren";
            MessageBoxManager.Register();
            Factory = new ViewUriFactory();
            CurrentPage = Factory.GetUri("LoginView");
            UserPrivilegeList = new RelayCommand(OpenUserPrivilegeListView, CanAccessUserPrivilegeListView);
            UserList = new RelayCommand(OpenUserListView, CanAccessUserListView);
            PrivilegeList = new RelayCommand(OpenPrivilegeListView, CanAccessPrivilegeListView);
            Logout = new RelayCommand(OpenLogout);

        }

        //After succesful login
        public void Login(AccountViewModel account)
        {
            Account = account;
            TopText = "Ingelogd als: " + Account.Name;
            RaisePropertyChanged("TopText");
            OpenUserPrivilegeListView();
            UpdateAcces();
        }

        #region OpenViews

        public void OpenUserPrivilegeListView()
        {
            CurrentPage = Factory.GetUri("UserPrivilegeListView");
        }

        public void OpenPrivilegeListView()
        {
            CurrentPage = Factory.GetUri("PrivilegeListView");
        }

        public void OpenUserListView()
        {
            CurrentPage = Factory.GetUri("UserListView");
        }

        public void OpenUserEditView()
        {
            CurrentPage = Factory.GetUri("UserEditView");
        }

        public void OpenPrivilegeEditView()
        {
            CurrentPage = Factory.GetUri("PrivilegeEditView");
        }

        public void OpenUserPrivilegeEditView()
        {
            CurrentPage = Factory.GetUri("UserPrivilegeEditView");
        }

        public void OpenChangePasswordView()
        {
            CurrentPage = Factory.GetUri("ChangePasswordView");
        }

        public void OpenLogout()
        {
            Account = null;
            TopText = "Inloggen";
            RaisePropertyChanged("TopText");
            CurrentPage = Factory.GetUri("LoginView");
            UpdateAcces();
        }

        #endregion OpenViews

        #region MainButtonsAccesability

        public void UpdateAcces()
        {
            RaisePropertyChanged(() => CanAccessUserPrivilegeListView);
            RaisePropertyChanged(() => CanAccessUserListView);
            RaisePropertyChanged(() => CanAccessPrivilegeListView);
            RaisePropertyChanged(() => CanLogout);
        }

        public bool CanAccessUserPrivilegeListView
        {
            get
            {
                if (Account != null)
                {
                    return true;
                }
                return false;
            }
        }

        public bool CanAccessUserListView
        {
            get
            {
                if (Account != null)
                {
                    return true;
                }
                return false;
            }
        }

        public bool CanAccessPrivilegeListView
        {
            get
            {
                if (Account != null)
                {
                    return true;
                }
                return false;
            }
        }

        public bool CanLogout
        {
            get
            {
                if (Account != null)
                {
                    return true;
                }
                return false;
            }
        }

        #endregion MainButtonsAccesability
    }
}