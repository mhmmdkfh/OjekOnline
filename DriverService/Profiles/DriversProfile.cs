﻿using AutoMapper;
using DriverService.Dtos;
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
        }
    }
}