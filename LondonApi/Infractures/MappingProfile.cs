using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LondonApi.Models;

namespace LondonApi.Infractures
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RoomEntity, Room>().
                ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate/100.0m));
        }
    }
}
