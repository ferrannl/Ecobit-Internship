using EcobitStage.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcobitStage.Interface
{
    public interface IPrivilegeRepository
    {
        void Add(PrivilegeDTO p);
        void Delete(PrivilegeDTO p);
        void Update(PrivilegeDTO p);
        List<PrivilegeDTO> GetAll();
    }
}
