using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardenApp.Models
{
    [NotMapped]
    public class Image
    {
        public string PlanName { get; set; }
        public string PlanCategory { get; set; }
        public int MinQuantity { get; set; }
        public int CostPerSqft { get; set; }
        public int ExpectedDuration { get; set; }
        public  IFormFile ImageFile { get; set; }

    }
}
