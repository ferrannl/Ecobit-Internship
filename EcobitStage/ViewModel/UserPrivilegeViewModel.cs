﻿using Ecobit.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EcobitStage.ViewModel
{
    public class UserPrivilegeViewModel : ViewModelBase
    {
        #region Commands

        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand IsOverDateCommand { get; set; }
        public ICommand IsAlmostOverDateCommand { get; set; }
        public ICommand IsNotOverDateCommand { get; set; }
        public ICommand SelectedDateCommand { get; set; }
        #endregion Commands

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
        //Initialize... create Commandfunctions
        private void Initialize()
        {
            SelectedUser = null;
            SelectedPrivilege = null;
            ObservableUsers = new ObservableCollection<DataViewModel.User>();
            ObservablePrivileges = new ObservableCollection<DataViewModel.Privilege>();
            ObservableUserPrivileges = new ObservableCollection<DataViewModel.UserPrivilege>();
            RefreshCommand = new RelayCommand(Refresh);
            SearchCommand = new RelayCommand(Search);
            DeleteCommand = new RelayCommand(Delete);
            NewCommand = new RelayCommand(New);
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
            IsOverDateCommand = new RelayCommand(RefreshIsOverDate);
            IsAlmostOverDateCommand = new RelayCommand(RefreshIsAlmostOverDate);
            IsNotOverDateCommand = new RelayCommand(RefreshIsNotOverDate);
            Refresh();
        }

        //Search function
        private void Search()
        {
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                var query = SearchQuery.ToLower();
                ObservableUserPrivileges.Clear();
                foreach (DataViewModel.UserPrivilege up in _userprivileges.Where(up => up.Fullname.ToLower().Contains(query) || up.Privilege_Name.ToLower().Contains(query) || up.Email.ToLower().Contains(query)))
                {
                    ObservableUserPrivileges.Add(up);
                }
            }
            else
            {
                Refresh();
            }
        }
        public void LoseFocus()
        {
            Mouse.Capture(null);
        }

        //Create new UserPrivilege
        private void New()
        {
            SelectedUser = new DataViewModel.User(-1);
            SelectedPrivilege = new DataViewModel.Privilege();
            Edit();
        }

        //Add UserPrivilege (Button)
        private void Edit()
        {
            //Removed refresh to fix bug in Save();
            Refresh();
            SelectedUserPrivilege = new DataViewModel.UserPrivilege();
            CommonServiceLocator.ServiceLocator.Current.GetInstance<MainViewModel>().OpenUserPrivilegeEditView();
        }

        //Cancel, escape
        private void Cancel()
        {
            CommonServiceLocator.ServiceLocator.Current.GetInstance<MainViewModel>().OpenUserPrivilegeListView();
        }

        //Save newly created UserPrivilege
        private void Save()
        {
            // Inconsistent bug where SelectedUserPrivilege is NULL //
            // Occurs when: clicking on + button after selecting an item in the list.

            //Validate all fields
            bool saved = true;
            if (SelectedUser.ValidateUserPrivilege() && SelectedPrivilege.ValidateUserPrivilege() && SelectedUserPrivilege.Validate())
            {
                Ecobit.Domain.UserPrivilege addUserPrivilege = new Ecobit.Domain.UserPrivilege { User_ID = SelectedUser.ID, Privilege_Name = SelectedPrivilege.Name, StartDate = SelectedUserPrivilege.StartDate, EndDate = SelectedUserPrivilege.EndDate };
                using (var context = new EcobitDBEntities())
                {
                    List<Ecobit.Domain.UserPrivilege> listuserprivileges = new List<Ecobit.Domain.UserPrivilege>(context.UserPrivilege.ToList());
                    List<Ecobit.Domain.Privilege> listprivileges = new List<Ecobit.Domain.Privilege>(context.Privilege.ToList());
                    List<Ecobit.Domain.Privilege> listusers = new List<Ecobit.Domain.Privilege>(context.Privilege.ToList());

                    var existingUser = context.User.Where(u => u.E_mail.Equals(SelectedUser.SelectedEmail));
                    if (existingUser.Count() == 0)
                    {                    
                        //Check if User exists
                        saved = false;
                        MessageBox.Show("Gebruiker " + SelectedUser.SelectedEmail + " bestaat niet.", "Bestaat niet", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        addUserPrivilege.User_ID = existingUser.SingleOrDefault().ID;
                        SelectedUser.FirstName = existingUser.SingleOrDefault().FirstName;

                    }

                    //Check if Privilege exists
                    var existingPrivilege = context.Privilege.Where(s => s.Name.ToLower().Equals(SelectedPrivilege.SelectedName.ToLower()));
                    if (existingPrivilege.Count() == 0)
                    {
                        saved = false;
                        MessageBox.Show("Toegankelijkheid " + SelectedPrivilege.SelectedName + " bestaat niet.", "Bestaat niet", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        addUserPrivilege.Privilege_Name = existingPrivilege.SingleOrDefault().Name;
                        SelectedPrivilege.Name = existingPrivilege.SingleOrDefault().Name;

                    }
                    //Check if combination User and Privilege exists
                    foreach (Ecobit.Domain.UserPrivilege up in listuserprivileges)
                    {
                        if (up.Privilege_Name.ToLower() == addUserPrivilege.Privilege_Name.ToLower() && up.User_ID == addUserPrivilege.User_ID)
                        {
                            MessageBox.Show("Combinatie " + SelectedUser.FirstName + " - " + SelectedPrivilege.Name + " bestaat al.",
                                "Combinatie bestaat al", MessageBoxButton.OK);
                            saved = false;
                            //Break in case dupes occur
                            break;
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

        //Delete UserPrivilege
        private void Delete()
        {
            if (MessageBox.Show("Wil je " + SelectedUserPrivilege.Fullname + " - " + SelectedUserPrivilege.Privilege_Name + " verwijderen?",
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

        //Get and create fullname using User ID.
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

        //Get and set email using User ID
        public string GetEmailByID(int id)
        {
            string email = null;
            using (var context = new EcobitDBEntities())
            {
                List<Ecobit.Domain.UserPrivilege> list = new List<Ecobit.Domain.UserPrivilege>(context.UserPrivilege.ToList());

                var user = context.User.FirstOrDefault(i => i.ID == id);
                email = user.E_mail;
            }
            if (email != null)
            {
                return email;
            }
            else
            {
                return "geen_email";
            }
        }

        //Refresh all lists
        public void Refresh()
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
                    newUP.Email = GetEmailByID(newUP.User_ID);
                    newUP.OverDate();
                    _userprivileges.Add(newUP);
                    ObservableUserPrivileges.Add(newUP);
                }
            RefreshPrivileges();
            RefreshUsers();
            }
        }

        #region Nested refreshes

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

        private void RefreshIsOverDate()
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
                    newUP.Email = GetEmailByID(newUP.User_ID);
                    newUP.OverDate();
                    if (newUP.IsOverDate)
                    {
                        _userprivileges.Add(newUP);
                        ObservableUserPrivileges.Add(newUP);
                    }
                }
            }
            RefreshPrivileges();
            RefreshUsers();
        }

        private void RefreshIsAlmostOverDate()
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
                    newUP.Email = GetEmailByID(newUP.User_ID);
                    newUP.OverDate();
                    if (newUP.IsAlmostOverDate)
                    {
                        _userprivileges.Add(newUP);
                        ObservableUserPrivileges.Add(newUP);
                    }
                }
            }
            RefreshPrivileges();
            RefreshUsers();
        }

        private void RefreshIsNotOverDate()
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
                    newUP.Email = GetEmailByID(newUP.User_ID);
                    newUP.OverDate();
                    if (newUP.IsNotOverDate)
                    {
                        _userprivileges.Add(newUP);
                        ObservableUserPrivileges.Add(newUP);
                    }
                }
            }
            RefreshPrivileges();
            RefreshUsers();
        }

        #endregion Nested refreshes
    }
}