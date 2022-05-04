using AutoMapper;
using BLL.Dtos;
using PL.Models;
using System.Collections.Generic;

namespace Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<EventDto, EventModel>().ReverseMap();
        }
    }
}