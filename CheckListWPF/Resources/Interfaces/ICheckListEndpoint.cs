using CheckListApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckListWPF.Resources.Interfaces
{
    public interface ICheckListEndpoint
    {
        Task<IEnumerable<Canvas>> GetUserCanvasList();
    }
}
