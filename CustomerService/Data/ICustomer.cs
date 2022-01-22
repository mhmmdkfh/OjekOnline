using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerService.Dtos;
using Microsoft.AspNetCore.Identity;
namespace CustomerService.Data
{
    public interface ICustomer
    {
        Task Register(RegisterInput body);
        Task<LoginResponse> Login(LoginInput body);
    }
}
