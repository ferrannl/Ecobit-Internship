using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcobitStage.DataTransfer;
using GalaSoft.MvvmLight;

namespace EcobitStage.ViewModel.DataViewModel
{
    public class User : ViewModelBase, ITransfer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private string _email;
        public string Email { get { return _email; } set { _email = value.ToLower(); } }


        public User(UserDTO DTO)
        {
            ID = DTO.ID;
            FirstName = DTO.FirstName;
            LastName = DTO.LastName;
            Email = DTO.Email;
        }
        public User(int ID, string Name, string Contact, string TelephoneNumber, string Email, string Postcode, int HouseNumber, string City)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
        }
            public DTO ConvertToDTO()
        {
            return new UserDTO(ID, FirstName, LastName, Email);
        }
    }
}
