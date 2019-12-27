﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EcobitStage.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //public static string baseUrl = "https://ecobit_iets.azurewebsites.net/";

        public static string baseUrl = "http://localhost:54109/";

        #region properties & fields

        private Uri _currentPage;
        [System.ComponentModel.Bindable(true)]
        public Uri CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                RaisePropertyChanged("CurrentPage");
            }
        }
        public ViewUriFactory factory { get; set; }
        public AccountViewModel Account { get; set; }
        public string TopText { get; set; }

        //Menubar Commands and CanExecutes.
        public ICommand UserPrivilegeList { get; set; }
        public ICommand PrivilegeList { get; set; }
        public ICommand UserList { get; set; }
        public ICommand Logout { get; set; }

        #endregion

        public MainViewModel()
        {
            factory = new ViewUriFactory();
            CurrentPage = factory.GetUri("LoginView");
            UserPrivilegeList = new RelayCommand(OpenUserPrivilegeListView, CanAccessUserPrivilegeListView);
            UserList = new RelayCommand(OpenUserListView, CanAccessUserListView);
            PrivilegeList = new RelayCommand(OpenPrivilegeListView, CanAccessPrivilegeListView);
            Logout = new RelayCommand(OpenLogout);
        }

        public void Login(AccountViewModel account)
        {
            Account = account;
            TopText = "Ingelogd als: " + Account.Name;
            RaisePropertyChanged("TopText");
            OpenUserPrivilegeListView();
            UpdateAcces();
        }

        public void OpenUserPrivilegeListView()
        {
            CurrentPage = factory.GetUri("UserPrivilegeListView");
        }

        public void OpenUserListView()
        {
            CurrentPage = factory.GetUri("UserListView");
        }

        public void OpenAccountView()
        {
            CurrentPage = factory.GetUri("UserView");
        }


        public void OpenPrivilegeListView()
        {
            CurrentPage = factory.GetUri("PrivilegeListView");
        }

        public void OpenUserEditView()
        {
            CurrentPage = factory.GetUri("UserEditView");
        }

        private void OpenLogout()
        {
            Account = null;
            TopText = "Inloggen";
            RaisePropertyChanged("TopText");
            CurrentPage = factory.GetUri("LoginView");
            UpdateAcces();
        }

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
    }
}