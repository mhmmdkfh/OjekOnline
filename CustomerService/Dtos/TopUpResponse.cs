using System.ComponentModel.DataAnnotations;

namespace CustomerService.Dtos
{
    public class TopUpResponse
    {
        public string Username { get; set; }
        public int Saldo { get; set; }
    }
}
