using Ecobit.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcobitStage.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        public ObservableCollection<User> ObservableUsers { get; set; }
        public User SelectedUser { get; set; }

        private List<User> users = new List<User>();


    }
}
