using AutoMapper;
using SohatNotebook.Entities.DbSet;
using SohatNotebook.Entities.Dto.Incoming;

namespace SohatNotebook.Api.Profiles
{
    public class UserProfile : Profile  
    {
        public UserProfile()
        {
            CreateMap<UserDto, UserDb>()
                .ForMember(dest => dest.FirstName, from => from.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.LastName, from => from.MapFrom(x => x.LastName))
                .ForMember(dest => dest.Email, from => from.MapFrom(x => x.Email))
                .ForMember(dest => dest.Phone, from => from.MapFrom(x => x.Phone))
                .ForMember(dest => dest.Country, from => from.MapFrom(x => x.Country))
                .ForMember(dest => dest.Status, from => from.MapFrom(x => 1))
                .ForMember(dest => dest.DateOfBirth, from => from.MapFrom(x => x.DateOfBirth));

            CreateMap<UserDb, UserDto>()
                .ForMember(dest => dest.FirstName, from => from.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.LastName, from => from.MapFrom(x => x.LastName))
                .ForMember(dest => dest.Email, from => from.MapFrom(x => x.Email))
                .ForMember(dest => dest.Phone, from => from.MapFrom(x => x.Phone))
                .ForMember(dest => dest.Country, from => from.MapFrom(x => x.Country))
                .ForMember(dest => dest.Status, from => from.MapFrom(x => 1))
                .ForMember(dest => dest.DateOfBirth, from => from.MapFrom(x => x.DateOfBirth));

            CreateMap<UserDb, UpdateProfileDto>()
                .ForMember(dest => dest.Country, from => from.MapFrom(x => x.Country))
                .ForMember(dest => dest.Address, from => from.MapFrom(x => x.Address))
                .ForMember(dest => dest.MobileNumber, from => from.MapFrom(x => x.MobileNumber))
                .ForMember(dest => dest.Gender, from => from.MapFrom(x => x.Gender));
        }
    }
}
