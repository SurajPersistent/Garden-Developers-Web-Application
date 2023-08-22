using GardenApi.Data;
using GardenApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;
        public AdminsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Admin>>> Get()
        {
            return Ok(await _dbContext.Admins.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> Get(int id)
        {
            var admin = await _dbContext.Admins.FindAsync(id);
            if (admin == null)
                return BadRequest("Record not found.");
            return Ok(admin);
        }

        [HttpPost]
        public async Task<ActionResult<List<Admin>>> Post(Admin admin)
        {
            _dbContext.Admins.Add(admin);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Admins.ToListAsync());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Admin>>> Delete(int id)
        {
            var admin = await _dbContext.Admins.FindAsync(id);
            if (admin == null)
                return BadRequest("Record not found.");

            _dbContext.Admins.Remove(admin);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Admins.ToListAsync());
        }
    }
}
