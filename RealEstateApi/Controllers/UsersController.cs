using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstateApi.Data;
using RealEstateApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstateApi.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       ApiDbContext _dbContext= new ApiDbContext();
        private IConfiguration _configuration;
        public UsersController(IConfiguration config)
        {
            _configuration = config;
            //access to details of appsettings file
        }

        [HttpPost("[Action]")]
        //api/Users/Register
        public IActionResult Register([FromBody] User user)
        {
            //var userxist= _dbContext.users.Any(u=>u.Email==user.Email);
            var userxist = _dbContext.users.FirstOrDefault(u=>u.Email==user.Email);
            if (userxist == null)
            {
                _dbContext.users.Add(user);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            else
                return BadRequest("already registered");

        }
        [HttpPost("[Action]")]
        public IActionResult Login([FromBody] User user)
        {
            var currentuser = _dbContext.users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (currentuser == null) {
                return
                    StatusCode(StatusCodes.Status401Unauthorized);
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var credentials=   new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email)

            };
       
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims:claims,
                expires:DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
            var jwt=new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(jwt);



        }

    }
}
