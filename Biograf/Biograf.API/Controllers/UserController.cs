//using Biograf.Repo.DTOs;
//using Biograf.Repo.Interface;
//using Biograf.Repo.Models.Entities;
//using System.Linq;
//using Biograf.Repo.Repositories;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

//namespace Biograf.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly IUserRepo _userRepo;

//        public UserController(IUserRepo userRepo)
//        {
//            _userRepo = userRepo;
//        }
//        [HttpGet]
//        public async Task<List<User>> Get()
//        {
//            return await _userRepo.Get();
//        }
//        [HttpGet("{id}")]
//        public async Task<IActionResult> Get(int id)
//        {
//            return Ok(await _userRepo.Get(id));
//        }

//        [HttpPost("registration")]
//        public async Task<IActionResult> Register(UserDto user)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            try
//            {
//                var newobj = new User
//                {
//                    FullName = user.FullName,
//                    Email = user.Email,
//                    Phone = user.Phone,
//                    Address = user.Address,
//                    Password = user.Password,
//                    Role = user.Role,
//                };

//                return Ok(await _userRepo.Create(newobj));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Failed to register {ex.Message}");
//            }


//        }
//        //[HttpPost("login")]
//        //public async Task<IActionResult> Login(LoginDto user)
//        //{
//        //    if (!ModelState.IsValid)
//        //    {
//        //        return BadRequest(ModelState);
//        //    }

//        //    var user = _userRepo.Get().FirstOrDefault(u =>
//        //           u.UserName == loginDto.UserName &&
//        //           u.Password == loginDto.Password &&
//        //           u.Role == loginDto.Role);
//        //    if (user == null)
//        //    {
//        //        return Unauthorized("Invalid email or password");
//        //    }
//        //    return Ok(existinguser);
//        //}

//        [HttpDelete]
//        public async Task<IActionResult> Delete(int id)
//        {
//            return Ok(await _userRepo.Delete(id));
//        }
//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(int id, User user)
//        {

//            return Ok(await _userRepo.Update(id, user));
//        }
//        //[HttpPost("l")]
//        //public ActionResult Login([FromBody] LoginDto loginDto)
//        //{
//        //    try
//        //    {
//        //        var user = _userRepo.Get().(u =>
//        //            u.UserName == loginDto.UserName &&
//        //            u.Password == loginDto.Password
//        //        );

//        //        if (user == null)
//        //        {
//        //            return Unauthorized("Invalid username or password");
//        //        }

//        //        // Assuming user.UserId is of type int
//        //        return Ok(new { Message = "Authentication successful", Id = user.UserId });
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return BadRequest(ex.Message);
//        //    }
//        //}
//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
//        {
//            try
//            {
//                var user = await _userRepo.getByemail(loginDto.Email);

//                if (user == null || user.Password != loginDto.Password)
//                {
//                    return Unauthorized("Invalid email or password");
//                }

//                return Ok(new { Message = "Authentication successful", Id = user.Id });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//    }





//}

