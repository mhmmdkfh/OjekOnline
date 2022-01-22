﻿using AdminService.Data.Dto;
using AdminService.Models;
using AutoMapper;

namespace AdminService.Data.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderData>()
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => $"{src.Lat} {src.Long}"));
        }
    }
}
