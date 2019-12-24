using EcobitStage.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcobitStage.Interface
{
    public interface IAccountRepository
    {
        void Add(AccountDTO a);
        void Delete(AccountDTO a);
        void Update(AccountDTO a);
        List<AccountDTO> GetAll();
    }
}
