namespace DriverService.Dtos.Order
{
    public class FinishOrderDto
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public string CustomerId { get; set; }
        public float Saldo { get; set; }
        public bool IsFinished { get; set; }
    }
}
