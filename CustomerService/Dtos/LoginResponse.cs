using System.ComponentModel.DataAnnotations;

namespace CustomerService.Dtos
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
