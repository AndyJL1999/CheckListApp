using CheckListApi.DTOs;
using CheckListApi.Models;

namespace CheckListApi.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> Register(User user, string password);
        Task<UserDto> Login(LoginDto loginDto);
        Task<bool> DoesUserExist(string username);
        bool ValidateEmail(string email);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
