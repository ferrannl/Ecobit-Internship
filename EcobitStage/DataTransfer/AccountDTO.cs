namespace EcobitStage.DataTransfer
{
    public class AccountDTO : DTO
    {
        public AccountDTO()
        {
        }

        public AccountDTO(int ID, string Name, string Username, string Password, string Role)
        {
            this.ID = ID;
            this.Name = Name;
            this.Username = Username;
            this.Password = Password;
            this.Role = Role;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}