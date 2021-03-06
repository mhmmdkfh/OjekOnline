using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DriverService.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public int NIK { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        public float Wallet { get; set; }
        [Required]
        [MaxLength(25)]
        public int Phone { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsAccepted { get; set; }
        

        public ICollection<Order> Orders { get; set; }
    }
}
