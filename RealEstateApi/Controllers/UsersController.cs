using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Data;
using RealEstateApi.Models;

namespace RealEstateApi.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       ApiDbContext _dbContext= new ApiDbContext();

        [HttpPost("[Action]")]
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

    }
}
