﻿using AdminService.Data.Dto;
using AdminService.Models;
using AutoMapper;

namespace AdminService.Data.Profiles
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, DriverData>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => $"{src.Lat} {src.Long}"));
        }
    }
}