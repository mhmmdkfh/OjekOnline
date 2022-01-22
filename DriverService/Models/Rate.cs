using System.ComponentModel.DataAnnotations;

namespace DriverService.Models
{
    public class Rate
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int TravelFares { get; set; }
    }
}
