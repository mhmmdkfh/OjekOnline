using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerService.Dtos;
using CustomerService.Models;
using Microsoft.AspNetCore.Identity;
namespace CustomerService.Data
{
    public interface ICustomer
    {
        Task Register(RegisterInput body);
        Task<LoginResponse> Login(LoginInput body);
        Task<IEnumerable<Customer>> GetAll();
        Task<CheckOrderFeeResponse> CheckOrderFee(CheckOrderFeeRequest request);
        Task<TopUpResponse> TopUp(TopUpRequest request);
        Task<ViewSaldoResponse> ViewSaldo();
        Task<IEnumerable<Order>> ViewOrderHistory();
        Task<Customer> UpdateWalletCustomer(string CustomerId, Customer obj);
        Task<Customer> LockUser(LockInput input);
        Task<Customer> GetById(string id);

    }
}
