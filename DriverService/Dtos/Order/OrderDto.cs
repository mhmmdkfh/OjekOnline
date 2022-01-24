namespace DriverService.Dtos.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public int CustomerId { get; set; }
        public double FromLat { get; set; }
        public double FromLong { get; set; }
        public double ToLat { get; set; }
        public double ToLong { get; set; }
        public double Rate { get; set; }
        public double Distance { get; set; }
        public float Price { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsFinished { get; set; }
    }
}
