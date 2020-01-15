namespace EcobitStage.DataTransfer
{
    public class PrivilegeDTO : DTO
    {
        public PrivilegeDTO(string Name)
        {
            this.Name = Name;
        }

        public string Name { get; set; }
    }
}