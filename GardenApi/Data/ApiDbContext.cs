using Microsoft.EntityFrameworkCore;
using GardenApp.Models;

namespace GardenApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        { }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Project> Projects { get; set; }
      //  public DbSet<Order> Orders { get; set; }
    }
}
