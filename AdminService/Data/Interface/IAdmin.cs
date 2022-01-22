using AdminService.Data.Dto;
using AdminService.Data.Dto.Input;
using AdminService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminService.Data.Interface
{
    public interface IAdmin
    {
        Task<Admin> RegisterAdmin(CreateAdmin create);
        Task<TokenUser> Login(LoginInput input);
        Task<Rate> SetPrice(float inputPrice);
        Task<Rate> GetPrice();

    }
}
