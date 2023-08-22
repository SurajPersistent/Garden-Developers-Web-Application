using System.ComponentModel.DataAnnotations;

namespace GardenApp.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long ContactNo { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}
