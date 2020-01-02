using Ecobit.Domain;
using System.Collections.Generic;
using System.Linq;
using EcobitStage.DataTransfer;
using EcobitStage.Interface;

namespace EcobitStage.Repository
{
    public class UserRepository : IUserRepository
    {
        List<UserDTO> users = new List<UserDTO>();

        public void Add(UserDTO u)
        {
            User addUser = new User { ID = u.ID, FirstName = u.FirstName, LastName = u.LastName, E_mail = u.Email };
            using (var context = new EcobitDBEntities())
            {
                context.User.Add(addUser);
                context.SaveChanges();
            }
        }

        public void Delete(UserDTO u)
        {
            using (var context = new EcobitDBEntities())
            {
                context.User.Remove(context.User.SingleOrDefault(us => us.ID == u.ID));
                context.SaveChanges();
            }
        }

        public List<UserDTO> GetAll()
        {
            using (var context = new EcobitDBEntities())
            {
                List<User> list = new List<User>(context.User.ToList());
                users.Clear();
                foreach(User u in list)
                {
                    users.Add(new UserDTO(u.ID, u.FirstName, u.LastName, u.E_mail));
                }
            }
            return users;
        }

        public void Update(UserDTO u)
        {
            throw new System.NotImplementedException();
        }
    }
}