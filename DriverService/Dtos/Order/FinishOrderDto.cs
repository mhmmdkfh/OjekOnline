﻿namespace DriverService.Dtos.Order
{
    public class FinishOrderDto
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public bool IsFinished { get; set; }
    }
}
