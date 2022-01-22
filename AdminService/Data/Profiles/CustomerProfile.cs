using AdminService.Data.Dto;
using AdminService.Models;
using AutoMapper;

namespace AdminService.Data.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerData>();
        }
    }
}
