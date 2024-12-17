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

            //region
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, AddRegionRequestDto>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDto>().ReverseMap();

            //walk
            CreateMap<Walk, AddWalkRequestDto>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Walk, UpdateWalkRequestDto>().ReverseMap();

            //difficulty
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
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

