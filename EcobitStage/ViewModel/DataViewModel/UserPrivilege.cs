using EcobitStage.DataTransfer;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcobitStage.ViewModel.DataViewModel
{
    public class UserPrivilege : ViewModelBase, ITransfer
    {
        public int User_ID { get; set; }
        public string Privilege_Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string _StartDate { get; set; }
        public string _EndDate { get; set; }
        public string UserFeedback { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Fullname { get; set; }


        public UserPrivilege(UserPrivilegeDTO DTO)
        {
            User_ID = DTO.User_ID;
            Privilege_Name = DTO.Privilege_Name;
            StartDate = DTO.StartDate;
            EndDate = DTO.EndDate;
        }
        public UserPrivilege(int User_ID, string Privilege_Name, DateTime StartDate, DateTime EndDate)
        {
            this.User_ID = User_ID;
            this.Privilege_Name = Privilege_Name;
            if (StartDate.Year >= 1000)
            {
                this._StartDate = ConvertDate(StartDate);
            }
            else
            {
                this._StartDate = ConvertDate(DateTime.Today);
            }
            if (StartDate.Year >= 1000)
            {
                this._EndDate = ConvertDate(EndDate);
            }
            else
            {
                this._EndDate = ConvertDate(DateTime.Today.AddDays(1));
            }
        }
        public UserPrivilege()
        {

        }

        internal bool Validate()
        {
            bool canSave = true;
            UserFeedback = "";

            if (string.IsNullOrWhiteSpace(Privilege_Name))
            {
                UserFeedback += "\r\n Het veld Naam is vereist.";
                canSave = false;
                Console.WriteLine("FM");
                Console.ReadLine();
            }

            if (UserFeedback.Length != 0)
            {
                string substringUserFeedback = UserFeedback.Substring(2);
                UserFeedback = substringUserFeedback;
            }
            RaisePropertyChanged(() => UserFeedback);
            return canSave;
        }

        public string ConvertDate(DateTime date)
        {
            string newDate = date.ToString("dd-MM-yyyy");
            return newDate;
        }

        public DTO ConvertToDTO()
        {
            return new UserPrivilegeDTO(User_ID, Privilege_Name, StartDate, EndDate );
        }
    }
}
