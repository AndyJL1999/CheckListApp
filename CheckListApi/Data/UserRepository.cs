using CheckListApi.DTOs;
using CheckListApi.Interfaces;
using CheckListApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace CheckListApi.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IAuthRepository _authRepo;

        public UserRepository(DataContext context, IAuthRepository authRepo)
        {
            _context = context;
            _authRepo = authRepo;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<string> UpdateUser(UpdateUserDto userUpdate, int userId)
        {
            if (userUpdate.Username.IsNullOrEmpty())
            {
                return "Please enter a username";
            }

            if ((_authRepo.ValidateEmail(userUpdate.Email) == false) && (userUpdate.Password.Length < 8))
            {
                return "Invalid email and password";
            }

            if (_authRepo.ValidateEmail(userUpdate.Email) == false)
            {
                return "Invalid email";
            }

            if (userUpdate.Password.Length < 8)
            {
                return "Password must be at least 8 characters";
            }

            _authRepo.CreatePasswordHash(userUpdate.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            user.Id = userId;
            user.Username = userUpdate.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Email = userUpdate.Email;

            await _context.SaveChangesAsync();

            return "Updated Successfully!";
        }
    }
}
