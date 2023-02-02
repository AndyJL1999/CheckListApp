using CheckListApi.DTOs;
using CheckListApi.Models;

namespace CheckListApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<string> UpdateUser(UpdateUserDto userUpdate, int userId);
    }
}
