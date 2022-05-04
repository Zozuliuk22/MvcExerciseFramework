using AutoMapper;
using BLL.DTOs;
using PL.Models;

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