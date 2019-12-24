using Ecobit.Domain;
using System.Collections.Generic;
using System.Linq;
using EcobitStage.DataTransfer;
using EcobitStage.Interface;
using System.Security.Cryptography;
using System.Text;

namespace EcobitStage.Repository
{
    public class AccountRepository : IAccountRepository
    {
        List<AccountDTO> accounts = new List<AccountDTO>();

        public void Add(AccountDTO a)
        {
            string hash;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, a.Password);
            }

            Account addAccount = new Account { ID = a.ID, Name = a.Name, Username = a.Username, Password = hash, Role = a.Role };
            using (var context = new EcobitDBEntities())
            {
                context.Account.Add(addAccount);
                context.SaveChanges();
            }
        }

        public void Delete(AccountDTO a)
        {
            using (var context = new EcobitDBEntities())
            {
                context.Account.Remove(context.Account.SingleOrDefault(ac => ac.ID == a.ID));
                context.SaveChanges();
            }
        }

        public AccountDTO Get(int id)
        {
            using (var context = new EcobitDBEntities())
            {
                Account a = context.Account.SingleOrDefault(ac => ac.ID == id);
                return new AccountDTO(a.ID, a.Name, a.Username, a.Password, a.Role);
            }
        }

        public List<AccountDTO> GetAll()
        {
            using (var context = new EcobitDBEntities())
            {
                List<Account> list = new List<Account>(context.Account.ToList());
                accounts.Clear();
                foreach (Account a in list)
                {
                    accounts.Add(new AccountDTO(a.ID, a.Name, a.Username, a.Password, a.Role));
                }
            }
            return accounts;
        }

        public void Update(AccountDTO a)
        {
            throw new System.NotImplementedException();
        }
        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
