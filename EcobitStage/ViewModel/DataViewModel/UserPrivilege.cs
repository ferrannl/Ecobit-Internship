using EcobitStage.DataTransfer;
using GalaSoft.MvvmLight;
using System;

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
        public string Fullname { get; set; }
        public bool IsOverDate { get; set; }
        public bool IsAlmostOverDate { get; set; }
        public bool IsNotOverDate { get; set; }

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
            //Setting startdate to today for convience
            if (StartDate.Year >= 1000)
            {
                this._StartDate = ConvertDate(StartDate);
            }
            else
            {
                this._StartDate = ConvertDate(DateTime.Today);
            }
            if (EndDate.Year >= 1000)
            {
                this._EndDate = ConvertDate(EndDate);
            }
            else
            {
                this._EndDate = ConvertDate(DateTime.Today.AddDays(1));
            }
            OverDate();
        }

        public UserPrivilege()
        {
            if (StartDate.Year >= 1000)
            {
                this._EndDate = ConvertDate(EndDate);
            }
            else
            {
                this._EndDate = ConvertDate(DateTime.Today.AddDays(1));
            }
            OverDate();
        }

        public void OverDate()
        {
            IsOverDate = false;
            IsAlmostOverDate = false;
            IsNotOverDate = false;
            if (Convert.ToDateTime(_EndDate) <= DateTime.Today.AddDays(7))
            {
                IsAlmostOverDate = true;
            }
            if (Convert.ToDateTime(_EndDate) <= DateTime.Today)
            {
                IsAlmostOverDate = false;
                IsOverDate = true;
            }
            if (Convert.ToDateTime(_EndDate) >= DateTime.Today.AddDays(7))
            {
                IsNotOverDate = true;
                IsAlmostOverDate = false;
                IsOverDate = false;
            }
            return;
        }

        #region Validate
        //Check for empty or invalid fields
                internal bool Validate()
        {
            bool canSave = true;
            UserFeedback = "";
            if (StartDate == null)
            {
                UserFeedback += "\r\n Het veld Startdatum is vereist.";
                canSave = false;
            }
            if (EndDate != null && EndDate <= StartDate)
            {
                UserFeedback += "\r\n Het veld Einddatum moet na Startdatum zijn.";
                canSave = false;
            }

            if (UserFeedback.Length != 0)
            {
                string substringUserFeedback = UserFeedback.Substring(2);
                UserFeedback = substringUserFeedback;
            }
            RaisePropertyChanged(() => UserFeedback);
            return canSave;
        }

        #endregion

        public string ConvertDate(DateTime date)
        {
            string newDate = date.ToString("dd-MM-yyyy");
            return newDate;
        }

        public DTO ConvertToDTO()
        {
            return new UserPrivilegeDTO(User_ID, Privilege_Name, StartDate, EndDate);
        }
    }
}