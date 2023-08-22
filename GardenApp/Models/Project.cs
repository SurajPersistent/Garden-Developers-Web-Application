using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardenApp.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

    }
}
