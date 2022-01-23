namespace AdminService.Data.Dto
{
    public class DriverData
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public float Wallet { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public bool IsAccepted { get; set; }
    }
}
