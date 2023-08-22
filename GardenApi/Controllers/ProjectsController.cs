using GardenApi.Data;
//using GardenApi.Helper;
using GardenApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;
        public ProjectsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Project>>> Get()
        {
            return Ok(await _dbContext.Projects.ToListAsync());
        }

       
        [HttpPost]
        public async Task<ActionResult<List<Project>>> Post(Project plan)
        {
            _dbContext.Projects.Add(plan);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Projects.ToListAsync());
        }

    }
}
