﻿using AdminService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminService.Data.Interface
{
    public interface IAdmin
    {
        // Driver
        Task<IEnumerable<Driver>> GetDrivers();
        Task<Driver> Approve(int id);
        Task<Driver> LockDriver(int id);

        // User
        Task<IEnumerable<Customer>> GetUsers();
        Task<Customer> LockUser(int id);

        //Order
        Task<IEnumerable<Order>> GetAllTransaction();
        Task<Order> SetPrice(Driver driver);
    }
}
