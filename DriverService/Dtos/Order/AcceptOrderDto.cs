namespace DriverService.Dtos.Order
{
    public class AcceptOrderDto
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public bool IsAccepted { get; set; }
    }
}
