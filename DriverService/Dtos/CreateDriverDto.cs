using System.ComponentModel.DataAnnotations;

namespace DriverService.Dtos
{
    public class CreateDriverDto
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Password { get; set; }
    }
}
