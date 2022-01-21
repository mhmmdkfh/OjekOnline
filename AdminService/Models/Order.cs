using System.ComponentModel.DataAnnotations;

namespace AdminService.Models
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
        public double Lat { get; set; }
        [Required]
        public double Long { get; set; }
        [Required]
        public float Price { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsFinished { get; set; }

        public Driver Driver { get; set; }
        public Customer Customer { get; set; }
    }
}
