namespace DriverService.Dtos.Order
{
    public class FinishOrderInput
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public float Saldo { get; set; }
    }
}
