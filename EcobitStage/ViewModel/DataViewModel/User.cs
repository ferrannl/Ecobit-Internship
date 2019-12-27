using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EcobitStage.DataTransfer;
using GalaSoft.MvvmLight;

namespace EcobitStage.ViewModel.DataViewModel
{
    public class User : ViewModelBase, ITransfer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private string _email;
        public string Email { get { return _email; } set { _email = value.ToLower(); } }
        public string UserFeedback { get; set; }


        public User(UserDTO DTO)
        {
            ID = DTO.ID;
            FirstName = DTO.FirstName;
            LastName = DTO.LastName;
            Email = DTO.Email;
        }
        public User(int ID, string Name, string Contact, string TelephoneNumber, string Email, string Postcode, int HouseNumber, string City)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
        }
        public User(int ID)
        {
            this.ID = ID;
        }

        internal bool Validate()
        {
            bool canSave = true;
            UserFeedback = "";

            if (string.IsNullOrWhiteSpace(FirstName))
            {
                UserFeedback += "\r\n Het veld Naam is vereist.";
                canSave = false;
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                UserFeedback += "\r\n Het veld Achternaam is vereist.";
                canSave = false;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                UserFeedback += "\r\n Het veld Email is vereist.";
                canSave = false;
            }
            else if (!Regex.IsMatch(Email, @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$"))
            {
                UserFeedback += "\r\n Het veld Email is niet valide.";
                canSave = false;
            }
           
            if (UserFeedback.Length != 0)
            {
                string substringUserFeedback = UserFeedback.Substring(2);
                UserFeedback = substringUserFeedback;
            }
            RaisePropertyChanged(() => UserFeedback);
            return canSave;
        }

        public DTO ConvertToDTO()
        {
            return new UserDTO(ID, FirstName, LastName, Email);
        }
    }
}
