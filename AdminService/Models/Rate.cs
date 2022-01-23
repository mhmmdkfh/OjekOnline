using System.ComponentModel.DataAnnotations;

namespace AdminService.Models
{
    public class Rate
    {
        [Key]
        [Required]
        public float TravelFares { get; set; }
    }
}
