using EcobitStage.DataTransfer;
using GalaSoft.MvvmLight;
using System.Text.RegularExpressions;

namespace EcobitStage.ViewModel.DataViewModel
{
    public class Privilege : ViewModelBase, ITransfer
    {
        public string Name { get; set; }
        public string UserFeedback { get; set; }
        public string SelectedName { get; set; }

        public Privilege(string Name)
        {
            this.Name = Name;
        }

        public Privilege()
        {
        }

        #region Validate

        internal bool Validate()
        {
            bool canSave = true;
            UserFeedback = "";

            if (string.IsNullOrWhiteSpace(Name))
            {
                UserFeedback += "\r\n Het veld `Toegankelijkheid` is vereist.";
                canSave = false;
            }
            else if (Regex.IsMatch(Name, @"[a-zA-Z]"))
            {
                canSave = true;
            }
            else
            {
                UserFeedback += "\r\n Het veld `Naam` is niet valide.";
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

            if (string.IsNullOrWhiteSpace(SelectedName))
            {
                UserFeedback += "\r\n Het veld `Toegankelijkheid` is vereist.";
                canSave = false;
            }
            else if (Regex.IsMatch(SelectedName, @"[a-zA-Z]"))
            {
                canSave = true;
            }
            else
            {
                UserFeedback += "\r\n Het veld `Naam` is niet valide.";
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
            return new PrivilegeDTO(Name);
        }
    }
}