using System.ComponentModel.DataAnnotations;

namespace CustomerService.Models
{
    public class Order
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int DriverId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        //Set Position Customer
        [Required]
        public double FromLat { get; set; }
        [Required]
        public double FromLong { get; set; }
        [Required]
        public double ToLat { get; set; }
        [Required]
        public double ToLong { get; set; }
        public double Rate { get; set; }
        public double Distance { get; set; }
        [Required]
        public float Price { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsFinished { get; set; }
    }
}
