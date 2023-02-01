using CheckListWPF.Resources.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckListWPF.Resources
{
    public class DataModel : IDataModel
    {
        public DataModel()
        {
            Data = string.Empty;
        }

        public DataModel(string data)
        {
            Data = data;
        }

        public string Data { get; set; }

        public string? Reverse()
        {
            char[] charArray = Data.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
