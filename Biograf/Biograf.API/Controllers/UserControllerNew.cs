using Biograf.API.Authorization;
using Biograf.Repo.DTOs;
using Biograf.Repo.Interface;
using Biograf.Repo.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace Biograf.API.Controllers.ControllerForGenericRepo
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserControllerNew : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IJwtUtils _jwtUtils;

        public UserControllerNew(IUserRepo userRepo, IJwtUtils jwtUtils)
        {
            _userRepo = userRepo;
            _jwtUtils = jwtUtils;
        }
        [HttpPost("Login")]
        //[AllowAnonymous] // Allow anonymous access to this endpoint
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            // Authenticate user
            User user = await _userRepo.getByemail(login.Email);

            if (user == null || user.Password != login.Password)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            // Generate JWT token
            string token = _jwtUtils.GenerateJwtToken(user);

            // Return token along with customer details
            UserDto response = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Address = user.Address,
                Phone = user.Phone,
                Password = user.Password,
                Role = user.Role,
                Token = token
            };
            return Ok(response);
        }

        //[HttpPost("Register")]
        //public async Task<IActionResult> Register([FromBody] RegistrationUser registrationUser)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    User existingUser = await _userRepo.getByemail(registrationUser.Email);
        //    if (existingUser != null)
        //    {
        //        return Conflict(new { message = "Email is already taken" });
        //    }

        //    User newUser = new User
        //    {
        //        Email = registrationUser.Email,
        //        Password = registrationUser.Password,
        //        FullName = registrationUser.FullName,
        //        Address = registrationUser.Address,
        //        Phone = registrationUser.Phone,
        //        Role = "User" // or any default role you want to assign
        //    };

        //    return Ok(new { message = "User registered successfully" });
        //}

    }
}
