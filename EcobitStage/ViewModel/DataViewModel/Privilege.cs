using EcobitStage.DataTransfer;
using GalaSoft.MvvmLight;

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

        public Privilege()
        {
        }

        internal bool Validate()
        {
            bool canSave = true;
            UserFeedback = "";

            if (string.IsNullOrWhiteSpace(Name))
            {
                UserFeedback += "\r\n Het veld `Toegankelijkheid` is vereist.";
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