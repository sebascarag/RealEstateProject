using AutoMapper;
using RealEstate.Application.Features.Properties.Queries;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Property, PropertyFilteredDto>().
                ForMember(x => x.OwnerName, opt => opt.MapFrom(s => s.Owner.Name));
        }
    }
}
