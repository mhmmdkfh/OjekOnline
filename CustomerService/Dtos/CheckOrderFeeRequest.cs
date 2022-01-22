using System.ComponentModel.DataAnnotations;

namespace CustomerService.Dtos
{
    public class CheckOrderFeeRequest
    {
        [Required]
        public double FromLat { get; set; }
        [Required]
        public double FromLong { get; set; }
        [Required]
        public double ToLat { get; set; }
        [Required]
        public double ToLong { get; set; }
    }
}
