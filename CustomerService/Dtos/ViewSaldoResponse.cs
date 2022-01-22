using System.ComponentModel.DataAnnotations;

namespace CustomerService.Dtos
{
    public class ViewSaldoResponse
    {
        public string Username { get; set; }
        public int Saldo { get; set; }
    }
}
