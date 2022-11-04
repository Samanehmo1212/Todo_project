using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TodoProject.Data;
using TodoProject.Models;

namespace TodoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly TodoDbContext _DbContext;
        private IConfiguration _config;
        //  private readonly SignInManager<User> _signInManager;
        public UserController(TodoDbContext DbContext, IConfiguration config)//,SignInManager<User> signInManager)
        {
            _DbContext = DbContext;
            _config = config;
            // _signInManager = signInManager;
        }


        [HttpPost("signUp")]
        public IActionResult Register(AddUser signUp)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Id=Guid.NewGuid(),
                     Email = signUp.Email,
                    Password = signUp.Password,
                    CreatedTime = DateTime.Now,
                    UpdatedTime=DateTime.Now

                };
                _DbContext.Add(user);
                _DbContext.SaveChanges();
                return Ok("User Created Successfuly");
            }
            return BadRequest("Data is Not Valid");
        }


        [HttpPost("Signin")]
        public IActionResult Signin(AddUser adduser)
        {
            var user = _DbContext.User.SingleOrDefault(x => (x.Email == adduser.Email && x.Password == adduser.Password));
            if (user != null)
            {


                return Ok(GenerateJSONWebToken(user));
            }
            return BadRequest("User Not Exist");

        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPut("{username}")]
      // [Authorize]
        public IActionResult ChangePasswors(string username, AddUser adduser)
        {
            if (username == adduser.Email)
            {
                var user1 = _DbContext.User.SingleOrDefault(x => x.Email == username);
                {

                    if (user1 != null)
                    {
                        user1.Password = adduser.Password;
                        user1.UpdatedTime = DateTime.Now;
                        user1.CreatedTime = user1.CreatedTime;
                        _DbContext.Update(user1);
                        _DbContext.SaveChanges();
                        return Ok("Password changed successfully");

                    }
                    else
                        return BadRequest("User Not Exist");

                }
            }
            return Ok("Not Authorize");
        }
    }
}
