using EcobitStage.DataTransfer;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public User(int ID, string FirstName, string LastName, string Email)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
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

            RaisePropertyChanged(() => UserFeedback);
            return canSave;
        }

        public DTO ConvertToDTO()
        {
            throw new NotImplementedException();
        }

    }
}
