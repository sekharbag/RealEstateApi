using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Data;

namespace RealEstateApi.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class Categoriescontroller : Controller
    {
        ApiDbContext _dbContext = new ApiDbContext();
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(_dbContext.Catagories);
        }
        
    }
}
