using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstateApi.Data;
using System.Security.Claims;

namespace RealEstateApi.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class PropertiesController : Controller
    {
        ApiDbContext _dbcontext = new ApiDbContext();
        [HttpGet("{id}")]
        [Authorize]

        public IActionResult Getproperties(int id)
        {
           var propertiesresult= _dbcontext.properties.Where(c => c.CatagoryId == id);
            if (propertiesresult == null)
            {
                return NotFound();  
            }
            return Ok(propertiesresult);    


        }
        [HttpGet("propertyDetail")]
        [Authorize]
        /////Api/Properties/propertyDetail?id=2

        public IActionResult Getproperties_propertyid(int id)
        {
            var propertiesresult = _dbcontext.properties.FirstOrDefault(p=>p.Id==id);
            if (propertiesresult == null)
            {
                return NotFound();
            }
            return Ok(propertiesresult);


        }
        [HttpGet("Tredingproperty")]
        [Authorize]
        public IActionResult Gettrendingproperty()
        {
            var propertiesresult = _dbcontext.properties.Where(c => c.IsTrending==true);
            if (propertiesresult == null)
            {
                return NotFound();
            }
            return Ok(propertiesresult);


        }
        [HttpGet("searchprop")]
        [Authorize]

        public IActionResult Getserchproperty(string address)
        {
            var propertiesresult = _dbcontext.properties.Where(c => c.Address.Contains(address));
            if (propertiesresult == null)
            {
                return NotFound();
            }
            return Ok(propertiesresult);


        }
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Models.Property prop)
        {
            if (prop == null)
            {
                return NoContent();
            }
            else
            {
                var UserEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbcontext.users.FirstOrDefault(u => u.Email == UserEmail);
                if (user == null)
                {
                    return NotFound();

                }
                prop.IsTrending = false;
                prop.UserId = user.Id;
                _dbcontext.properties.Add(prop);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult put([FromBody] Models.Property prop, int id)
        {
            var propertyresult = _dbcontext.properties.FirstOrDefault(p => p.Id == id);

            if (propertyresult == null)
            {
                return NoContent();
            }
            else
            {
                var UserEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbcontext.users.FirstOrDefault(u => u.Email == UserEmail);
                if (user == null) return NotFound();
                if (propertyresult.UserId == user.Id)
                {
                    propertyresult.Address = prop.Address;
                    propertyresult.Price = prop.Price;
                    propertyresult.Detail = prop.Detail;
                    propertyresult.Name = prop.Name;
                    prop.IsTrending = false;
                    prop.UserId = user.Id;
                    _dbcontext.SaveChanges();
                    return Ok("updated");
                }
                return Ok("not possible");
            }
        }


        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete( int id)
        {
            var propertyresult = _dbcontext.properties.FirstOrDefault(p => p.Id == id);

            if (propertyresult == null)
            {
                return NoContent();
            }
            else
            {
                var UserEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbcontext.users.FirstOrDefault(u => u.Email == UserEmail);
                if (user == null) return NotFound();
                if (propertyresult.UserId == user.Id)
                {
                    _dbcontext.properties.Remove(propertyresult);
                    _dbcontext.SaveChanges();
                    return Ok("deleted");
                }
                return Ok("not possible");
            }
        }
    }
}
