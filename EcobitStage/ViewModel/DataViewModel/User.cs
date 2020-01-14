using EcobitStage.DataTransfer;
using GalaSoft.MvvmLight;
using System.Text.RegularExpressions;

namespace EcobitStage.ViewModel.DataViewModel
{
    public class User : ViewModelBase, ITransfer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserFeedback { get; set; }

        public User(UserDTO DTO)
        {
            ID = DTO.ID;
            FirstName = DTO.FirstName;
            LastName = DTO.LastName;
            Email = DTO.Email;
        }

        public User(int ID, string FirstName, string LastName, string Email)
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

        #region Validate

        //Check for empty or invalid fields
        internal bool Validate()
        {
            bool canSave = true;
            UserFeedback = "";

            if (string.IsNullOrWhiteSpace(FirstName))
            {
                UserFeedback += "\r\n Het veld `Voornaam` is vereist.";
                canSave = false;
            }
            else if (Regex.IsMatch(FirstName, @"^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$"))
            {
                canSave = true;
            }
            else
            {
                UserFeedback += "\r\n Het veld `Voornaam` is niet valide.";
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                UserFeedback += "\r\n Het veld `Achternaam` is vereist.";
                canSave = false;
            }
            else if (Regex.IsMatch(LastName, @"^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$"))
            {
                canSave = true;
            }
            else
            {
                UserFeedback += "\r\n Het veld `Achternaam` is niet valide.";
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                UserFeedback += "\r\n Het veld `Email` is vereist.";
                canSave = false;
            }
            else if (Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                canSave = true;
            }
            else
            {
                UserFeedback += "\r\n Het veld `Email` is niet valide.";
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

        internal bool ValidateUserPrivilege()
        {
            bool canSave = true;
            UserFeedback = "";

            if (string.IsNullOrWhiteSpace(Email))
            {
                UserFeedback += "\r\n Het veld `Gebruiker` is vereist.";
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

        #endregion Validate

        public DTO ConvertToDTO()
        {
            return new UserDTO(ID, FirstName, LastName, Email);
        }
    }
}