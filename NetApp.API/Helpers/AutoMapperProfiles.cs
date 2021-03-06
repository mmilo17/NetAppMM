using System.Linq;
using AutoMapper;
using NetApp.API.Dtos;
using NetApp.API.Models;

namespace NetApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserFroListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => 
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(Dest => Dest.Age, opt => 
                    opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, opt => 
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(Dest => Dest.Age, opt => 
                    opt.MapFrom(src => src.DateOfBirth.CalculateAge()));    
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>()
                .ForMember(m => m.MessageSenderPhotoUrl, opt => opt.MapFrom(u => u.MessageSender.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(m => m.MessageRecipientPhotoUrl, opt => opt.MapFrom(u => u.MessageRecipient.Photos.FirstOrDefault(p => p.IsMain).Url));
        }
    }
}