﻿namespace AdminService.Data.Dto
{
    public class OrderData
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public int CustomerId { get; set; }
        public string Destination { get; set; }
        public float Price { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsFinished { get; set; }
    }
}
