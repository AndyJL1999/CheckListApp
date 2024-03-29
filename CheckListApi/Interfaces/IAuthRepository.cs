﻿using CheckListApi.DTOs;
using CheckListApi.Models;

namespace CheckListApi.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> Register(RegisterDto userDto);
        Task<UserDto> Login(LoginDto loginDto);
        Task<bool> DoesUserExist(string username);
        bool ValidateEmail(string email);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
