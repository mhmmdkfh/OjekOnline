using System.ComponentModel.DataAnnotations;

namespace CustomerService.Dtos
{
    public class CheckOrderFeeResponse
    {
        [Required]
        public double FromLat { get; set; }
        [Required]
        public double FromLong { get; set; }
        [Required]
        public double ToLat { get; set; }
        [Required]
        public double ToLong { get; set; }
        public double Distance { get; set; }
        public double Rate { get; set; }
        public double Price { get; set; }
    }
}
