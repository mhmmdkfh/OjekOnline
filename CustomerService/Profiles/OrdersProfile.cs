using AutoMapper;
using CustomerService.Dtos;
using CustomerService.Models;

namespace CustomerService.Profiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<Order, PriceOrderDto>();
            CreateMap<PriceOrderDto, Order>();
            CreateMap<Customer, FinishOrderInput>();
            CreateMap<FinishOrderInput, Customer>();
            CreateMap<Customer, UpdateWalletDto>();
            CreateMap<UpdateWalletDto, Customer>();
        }   
    }
}
