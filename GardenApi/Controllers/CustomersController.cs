using GardenApi.Data;
using GardenApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;
        public CustomersController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Customer Table Get All records
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomer()
        {
            return Ok(await _dbContext.Customers.ToListAsync());
        }

        //Customer Table Get record by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null)
                return BadRequest("Record not found.");
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> Post(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Customers.ToListAsync());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Customer>>> Delete(int id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null)
                return BadRequest("Record not found.");
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Customers.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Customer>>> Update(Customer customerObj)
        {
            var customer = await _dbContext.Customers.FindAsync(customerObj.CustomerId);
            if (customer == null)
                return BadRequest("Record not found.");

            customer.Status = customerObj.Status;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Customers.ToListAsync());
        }
    }
}
