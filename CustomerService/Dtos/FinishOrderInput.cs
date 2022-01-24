namespace CustomerService.Dtos
{
    public class FinishOrderInput
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public bool IsFinished { get; set; }
        public string CustomerId { get; set; }
        public float Saldo { get; set; }
    }
}
