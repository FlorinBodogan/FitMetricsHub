using AutoMapper;
using BACKEND.DTOs.Admin;
using BACKEND.Entities;
using BACKEND.Extensions;
using BACKEND.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager, IUnitOfWork uow, IMapper mapper)
        {
            _userManager = userManager;
            _uow = uow;
            _mapper = mapper;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    UserName = u.UserName,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                })
                .ToListAsync();

            return Ok(users);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            if (string.IsNullOrEmpty(roles)) return BadRequest("You must select at least one role");

            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return NotFound();

            if (user.UserName == "admin") return BadRequest("You cannot modify the admin roles");

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded) return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded) return BadRequest("Failed to remove from roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("lock-member/{id}")]
        public async Task<IActionResult> LockMember([FromBody] ActionsUserDto actionsUserDto)
        {
            var user = await _userManager.FindByNameAsync(actionsUserDto.UserName);
            if (user == null) return NotFound();

            if (IsAdminUserName(actionsUserDto.UserName))
            {
                return BadRequest("You cannot ban admin");
            }

            await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddDays(5000));
            return NoContent();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("unlock-member/{id}")]
        public async Task<IActionResult> UnlockMember([FromBody] ActionsUserDto actionsUserDto)
        {
            var user = await _userManager.FindByNameAsync(actionsUserDto.UserName);
            if (user == null) return NotFound();

            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                await _userManager.SetLockoutEndDateAsync(user, null);
                return NoContent();
            }
            else
            {
                return BadRequest("User is not locked.");
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("delete-member/{id}")]
        public async Task<IActionResult> DeleteMember([FromBody] ActionsUserDto deleteUserDto)
        {
            var username = User.GetUsername();

            if (username != "admin")
            {
                return BadRequest("Only administrator can delete members.");
            }

            var user = await _userManager.FindByNameAsync(deleteUserDto.UserName);
            if (user == null) return NotFound();

            if (IsAdminUserName(deleteUserDto.UserName))
            {
                return BadRequest("You cannot delete admin");
            }

            await _uow.ArterialTensionRepository.DeleteUserATs(user.Id);
            await _uow.BmiRepository.DeleteUserBmis(user.Id);
            await _uow.RmbRepository.DeleteUserRmbs(user.Id);
            await _uow.TriglyceridesRepository.DeleteUserTriglycerides(user.Id);
            await _uow.CholesterolRepository.DeleteUserCholesterols(user.Id);

            await _userManager.DeleteAsync(user);
            return NoContent();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("edit-member-info/{username}")]
        public async Task<ActionResult> EditUserInfo(string username, [FromBody] UserUpdateInfoDto userUpdateDto)
        {

            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return NotFound();

            if (IsAdminUserName(username))
            {
                return BadRequest("You cannot edit admin");
            }

            if (!userUpdateDto.UserName.All(char.IsLetterOrDigit) && !userUpdateDto.UserName.All(char.IsLower))
            {
                return BadRequest("Username must consist of lowercase letters and digits.");
            }

            if (await UserExists(userUpdateDto.UserName, username)) return BadRequest("Username is taken.");

            user.UserName = userUpdateDto.UserName;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { message = "User information updated successfully." });
            }
            else
            {
                return BadRequest(new { message = "User information update failed." });
            }
        }

        //STATISTICS
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("usersCount")]
        public async Task<UsersCountDto> GetNumberOfUsers()
        {
            var users = await _uow.UserRepository.GetNumbersOfUsers();
            return users;
        }

        //UTILS
        private bool IsAdminUserName(string username)
        {
            return _userManager.FindByNameAsync(username).GetAwaiter().GetResult().UserName.Equals("admin");
        }

        private async Task<bool> UserExists(string username, string userReq)
        {
            return await _userManager.Users.AnyAsync(user => user.UserName == username.ToLower() && user.UserName != userReq);
        }
    }
}