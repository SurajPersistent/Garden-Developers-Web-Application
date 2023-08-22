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
    public class PlansController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;
        public PlansController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Plan>>> Get()
        {
            return Ok(await _dbContext.Plans.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Plan>> Get(int id)
        {
            var plan = await _dbContext.Plans.FindAsync(id);
            if (plan == null)
                return BadRequest("Record not found.");
            return Ok(plan);
        }

        [HttpPost]
        public async Task<ActionResult<List<Plan>>> Post(Plan plan)
        {
            _dbContext.Plans.Add(plan);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Plans.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Plan>>> Update(Plan planObj)
        {
            var plan = await _dbContext.Plans.FindAsync(planObj.PlanId);
            if (plan == null)
                return BadRequest("Record not found.");

            plan.CostPerSqft = planObj.CostPerSqft;
            plan.PlanCategory = planObj.PlanCategory;
            plan.PlanName = planObj.PlanName;
            plan.MinQuantity = planObj.MinQuantity;
            plan.ExpectedDuration = planObj.ExpectedDuration;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Plans.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Plan>>> Delete(int id)
        {
            var plan = await _dbContext.Plans.FindAsync(id);
            if (plan == null)
                return BadRequest("Record not found.");

            _dbContext.Plans.Remove(plan);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Plans.ToListAsync());
        }
    }
}
