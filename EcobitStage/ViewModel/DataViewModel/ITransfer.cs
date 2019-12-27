using EcobitStage.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcobitStage.ViewModel.DataViewModel
{
    public interface ITransfer
    {
        DTO ConvertToDTO();

    }
}