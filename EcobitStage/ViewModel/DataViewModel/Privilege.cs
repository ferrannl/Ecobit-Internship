using EcobitStage.DataTransfer;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcobitStage.ViewModel.DataViewModel
{
    public class Privilege : ViewModelBase, ITransfer
    {
        public string Name { get; set; }
        public string UserFeedback { get; set; }

        public Privilege(PrivilegeDTO DTO)
        {
            Name = DTO.Name;
        }
        public Privilege(string Name)
        {
            this.Name = Name;
        }


        internal bool Validate()
        {
            bool canSave = true;
            UserFeedback = "";

            if (string.IsNullOrWhiteSpace(Name))
            {
                UserFeedback += "\r\n Het veld Naam is vereist.";
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
            return new PrivilegeDTO(Name);
        }
    }
}
