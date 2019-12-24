using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcobitStage.DataTransfer
{
    public class PrivilegeDTO : DTO
    {
        public PrivilegeDTO()
        {
        }
        public PrivilegeDTO(string Name)
        {
            this.Name = Name;
        }
        public string Name { get; set; }

    }
}
