using Ecobit.Domain;
using EcobitStage.DataTransfer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EcobitStage.ViewModel
{
    public class ChangePasswordViewModel : ViewModelBase
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        AccountDTO tempAccount = new AccountDTO();
        public ICommand NewPasswordCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private MainViewModel _main;
        //AccountDTO tempAccount = new AccountDTO();
        public ChangePasswordViewModel(MainViewModel Main)
        {
            _main = Main;
            CancelCommand = new RelayCommand(_main.OpenLogout);
            NewPasswordCommand = new RelayCommand<PasswordBox>(ChangePassword);
        }

        public void ChangePassword(PasswordBox PasswordBox)
        {
            AccountDTO account = GetAccountByUsername(Username);
            if (account != null)
            {
                if (!string.IsNullOrWhiteSpace(OldPassword))
                {
                    if (!string.IsNullOrEmpty(PasswordBox.Password)){
                        if (VerifyPassword(account.ID))
                        {
                            //Do this after old passwords are correct
                            PasswordBox.Password = null;
                        }
                        else
                        {
                            MessageBox.Show("Inlog gegevens zijn incorrect.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Vul een nieuw wachtwoord in.");
                    }
                }
                else
                {
                    MessageBox.Show("Vul het oude wachtwoord in.");
                }
            }
            else
            {
                MessageBox.Show("Een account met deze gebruikersnaam bestaat niet.");
            }
        }

        public bool VerifyPassword(int id)
        {
            string hash;

            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, OldPassword);

            }

            {
                using (var context = new EcobitDBEntities())
                {
                    List<Account> list = new List<Account>(context.Account.ToList());
                    foreach (Account a in list)
                    {
                        if (a.ID == id && a.Password.Equals(hash))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }



        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public AccountDTO GetAccountByUsername(string username)
        {
            using (var context = new EcobitDBEntities())
            {
                List<Account> tempList = new List<Account>(context.Account.ToList());
                tempAccount = null;
                foreach (Account a in tempList)
                {
                    if (a.Username.Equals(username))
                    {
                        tempAccount = new AccountDTO(a.ID, a.Name, a.Username, a.Password, a.Role);
                        return tempAccount;
                    }
                }
                return null;
            }

        }

    }

}