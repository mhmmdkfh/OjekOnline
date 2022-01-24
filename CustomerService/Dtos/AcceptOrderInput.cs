namespace CustomerService.Dtos
{
    public class AcceptOrderInput
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public bool IsAccepted { get; set; }
        //public bool IsFinished { get; set; }
    }
}
