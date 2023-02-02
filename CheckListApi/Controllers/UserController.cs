using CheckListApi.Data;
using CheckListApi.DTOs;
using CheckListApi.Extentions;
using CheckListApi.Interfaces;
using CheckListApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckListApi.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUser()
        {
            var id = User.GetUserId();

            var user = await _userRepo.GetUserByIdAsync(id);

            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateUserDto user)
        {
            var id = User.GetUserId();
            var result = await _userRepo.UpdateUser(user, id);

            if (result != "Updated Successfully!")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
