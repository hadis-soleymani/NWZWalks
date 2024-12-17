using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<UserDto, UserDomain>()
            //    .ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName))
            //    .ReverseMap();

            CreateMap<Region, RegionDto>().ReverseMap();

        }

        //public class UserDto
        //{
        //    public string FullName { get; set; }
        //}

        //public class UserDomain
        //{
        //    public string Name { get; set; }
        //}
    }
}

