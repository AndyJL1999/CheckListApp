using CheckListWPF.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CheckListWPF.Resources.Interfaces
{
    public interface IApiHelper
    {
        Task<AuthenticatedUser> Authenticate(string email, string password);
        Task<string> Register(string username, string password, string email);
        Task GetUserInfo(string token);
        Task<string> UpdateUser(string username, string email);
        Task<string> UpdatePassword(string oldPassword, string newPassword);
        ILoggedInUser LoggedInUser { get; set; }
        HttpClient ApiClient { get; }
    }
}
