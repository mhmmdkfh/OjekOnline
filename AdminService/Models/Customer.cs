using System;
using System.ComponentModel.DataAnnotations;

namespace AdminService.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Saldo { get; set; }
        public bool IsActive { get; set; }
    }
}
