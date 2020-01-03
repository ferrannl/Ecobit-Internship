using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcobitStage.DataTransfer
{
    public class UserPrivilegeDTO : DTO
    {
        public UserPrivilegeDTO()
        {

        }

        public UserPrivilegeDTO(int User_ID, string Privilege_Name, DateTime StartDate, DateTime EndDate)
        {
            this.User_ID = User_ID;
            this.Privilege_Name = Privilege_Name;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }

        public int User_ID { get; set; }
        public string Privilege_Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
