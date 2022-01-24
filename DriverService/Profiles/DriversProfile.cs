using AutoMapper;
using DriverService.Dtos;
using DriverService.Dtos.Order;
using DriverService.Models;

namespace DriverService.Profiles
{
    public class DriversProfile : Profile
    {
        public DriversProfile()
        {
            CreateMap<Driver, CreateDriverDto>();
            CreateMap<CreateDriverDto, Driver>();
            CreateMap<Driver, DriverDto>();
            CreateMap<DriverDto, Driver>();
            CreateMap<Driver, WalletDriverDto>();
            CreateMap<Driver, SetLocationDriverDto>();
            CreateMap<SetLocationDriverDto, Driver>();
            CreateMap<Driver, UpdateWalletDto>();
            CreateMap<UpdateWalletDto, Driver>();
            CreateMap<Driver, FinishOrderInput>();
            CreateMap<FinishOrderInput, Driver>();
        }
    }
}
