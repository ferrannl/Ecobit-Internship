using Ecobit.Domain;
using EcobitStage.DataTransfer;
using EcobitStage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecobit.Repository.Repository
{
    public class PrivilegeRepository : IPrivilegeRepository
    {
        List<PrivilegeDTO> privileges = new List<PrivilegeDTO>();
        public void Add(PrivilegeDTO p)
        {
            Privilege addPrivilege = new Privilege { Name = p.Name };
            using (var context = new EcobitDBEntities())
            {
                context.Privilege.Add(addPrivilege);
                context.SaveChanges();
            }
        }

        public void Delete(PrivilegeDTO p)
        {
            using (var context = new EcobitDBEntities())
            {
                context.Privilege.Remove(context.Privilege.SingleOrDefault(pr => pr.Name == p.Name));
                context.SaveChanges();
            }
        }

        public List<PrivilegeDTO> GetAll()
        {
            using (var context = new EcobitDBEntities())
            {
                List<Privilege> list = new List<Privilege>(context.Privilege.ToList());
                privileges.Clear();
                foreach (Privilege p in list)
                {
                    privileges.Add(new PrivilegeDTO(p.Name));
                }
            }
            return privileges;
        }

        public void Update(PrivilegeDTO p)
        {
            throw new NotImplementedException();
        }
    }

}