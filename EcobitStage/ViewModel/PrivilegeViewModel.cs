using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcobitStage.ViewModel
{
    public class PrivilegeViewModel : ViewModelBase
    {
        public ObservableCollection<PrivilegeViewModel> Privilege { get; set; }
    }
}