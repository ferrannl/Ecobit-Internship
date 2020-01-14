using EcobitStage.DataTransfer;
using GalaSoft.MvvmLight;

namespace EcobitStage.ViewModel
{
    public class AccountViewModel : ViewModelBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public AccountViewModel(AccountDTO account)
        {
            this.ID = account.ID;
            this.Name = account.Name;
            this.Username = account.Username;
            this.Password = account.Password;
            this.Role = account.Role;
        }

        public AccountViewModel()
        {
            ID = -1;
        }

        public AccountDTO ToAccountDTO()
        {
            AccountDTO dto = new AccountDTO(ID, Name, Username, Password, Role);
            return dto;
        }
    }
}