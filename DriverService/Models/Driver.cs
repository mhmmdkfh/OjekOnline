﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DriverService.Models
{
    public class Driver
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Username { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        public float Wallet { get; set; }
        [Required]
        [MaxLength(25)]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsActive { get; set; }
        // Set Position Driver
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
