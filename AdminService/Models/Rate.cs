﻿using System.ComponentModel.DataAnnotations;

namespace AdminService.Models
{
    public class Rate
    {
        [Key]
        [Required]
        public int TravelFares { get; set; }
    }
}