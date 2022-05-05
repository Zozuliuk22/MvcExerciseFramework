using AutoMapper;
using BLL.Dtos;
using PL.Models;

namespace Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<EventDto, EventModel>().ReverseMap();
            Mapper.CreateMap<PlayerDto, PlayerViewModel>().ReverseMap();
        }
    }
}