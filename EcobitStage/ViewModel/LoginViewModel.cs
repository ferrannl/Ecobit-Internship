using Ecobit.Domain;
using EcobitStage.DataTransfer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EcobitStage.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public string Username { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        private MainViewModel _main;
        private AccountDTO tempAccount = new AccountDTO();

        //Initialize login view
        public LoginViewModel(MainViewModel Main)
        {
            _main = Main;
            LoginCommand = new RelayCommand<PasswordBox>(Login);
            ChangePasswordCommand = new RelayCommand(ChangePassword);
        }

        //Login verification
        private void Login(PasswordBox PasswordBox)
        {
            //Check username
            if (string.IsNullOrWhiteSpace(Username))
            {
                MessageBox.Show("Gebruikersnaam invullen a.u.b.");
                return;
            }

            AccountDTO account = GetAccountByUsername(Username);
            if (account != null)
            {
                //Check password
                if (VerifyPassword(account.ID, PasswordBox.Password))
                {
                    _main.Login(new AccountViewModel(account));
                    PasswordBox.Password = null;
                }
                else
                {
                    MessageBox.Show("Inlog gegevens zijn incorrect.");
                }
            }
            else
            {
                MessageBox.Show("Een account met deze gebruikersnaam bestaat niet.");
            }
        }

        private void ChangePassword()
        {
            _main.OpenChangePasswordView();
        }

        public bool VerifyPassword(int id, string password)
        {
            //Hashes password and check if it's correct
            string hash;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, password);
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

        //Hashes password
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