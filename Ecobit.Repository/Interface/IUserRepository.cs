using EcobitStage.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcobitStage.Interface
{
    public interface IUserRepository
    {
        void Add(UserDTO user);
        void Delete(UserDTO user);
        void Update(UserDTO user);
        List<UserDTO> GetAll();
    }
}
