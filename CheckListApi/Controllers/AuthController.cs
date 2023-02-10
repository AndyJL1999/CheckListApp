using AutoMapper;
using CheckListApi.DTOs;
using CheckListApi.Interfaces;
using CheckListApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckListApi.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authRepo.Register(registerDto);

            if (result != "User has been registered")
            {
                return BadRequest(result.ToString());
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto user)
        {
            var userLogin = await _authRepo.Login(user);

            if (userLogin == null)
            {
                return Unauthorized("Invalid user");
            }

            return Ok(userLogin);
        }
    }
}
