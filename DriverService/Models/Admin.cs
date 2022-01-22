using System.ComponentModel.DataAnnotations;

namespace DriverService.Models
{
    public class Admin
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string NIK { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(25)]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
