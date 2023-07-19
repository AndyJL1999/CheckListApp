using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckListWPF.Resources.Interfaces
{
    public interface ILoggedInUser
    {
        int Id { get; set; }
        string Email { get; set; }
        string Token { get; set; }
        string Username { get; set; }
        public string BackgroundColor { get; set; }
    }
}
