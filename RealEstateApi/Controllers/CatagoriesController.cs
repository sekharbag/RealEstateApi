using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateApi.Data;
using RealEstateApi.Models;

namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoriesController : Controller
    {
        ApiDbContext _dbcontext = new ApiDbContext();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbcontext.Catagories);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _dbcontext.Catagories.FirstOrDefault(x => x.id == id);
            if (category != null)
            {
                return Ok(category);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
        //atribute routing
        //api/Catagories/getsortcategories
        [HttpGet("[action]")]
        public IActionResult getsortcategories()
        {
            return Ok(_dbcontext.Catagories.OrderBy(x=>x.Name).Reverse());
        }
        [HttpPost]
        public IActionResult post([FromBody] Catagory catagory)
        {
            _dbcontext.Catagories.Add(catagory);
            _dbcontext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
       public IActionResult put(int id, [FromBody] Catagory catagoryobj)
        {
           var category= _dbcontext.Catagories.FirstOrDefault(x => x.id == id);
            if (category != null)
            {
                category.Name = catagoryobj.Name;
                category.ImageUrl = catagoryobj.ImageUrl;
                _dbcontext.SaveChanges();
                return Ok("record updated");
            }
            else 
                return NotFound("no record"+id); 

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _dbcontext.Catagories.FirstOrDefault(x => x.id == id);
            if (category != null)
            {
                _dbcontext.Catagories.Remove(category);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK);

            }
            else
                return BadRequest("not possible"+id);
        }
    }
}
