using AutoMapper;
using BussinessLayer;
using HaitenWebAPI.DTOs.Chapter;
using HaitenWebAPI.DTOs.ChapterText;
using HaitenWebAPI.DTOs.Comment;
using HaitenWebAPI.DTOs.Manga;
using HaitenWebAPI.DTOs.User;

namespace HaitenWebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<User, LoginDTO>()
                .ForMember(dest => dest.UserNameOrEmail, opt => opt.MapFrom(src => src.UserName)) 
                .ReverseMap();
            CreateMap<User, RegisterDTO>()
                .ReverseMap();

            CreateMap<User, UserDTO>()
              .ReverseMap();

            CreateMap<ChangePasswordDTO, User>()
           .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.NewPassword));

            CreateMap<Manga, MangaListDTO>()
            .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Rates.Any() ? src.Rates.Average(r => r.Rating) : 0));
            CreateMap<Manga, MangaDTO>()
            .ForMember(dest => dest.Chapters, opt => opt.MapFrom(src => src.Chapters));
            CreateMap<Chapter, ChapterDTO>();
            CreateMap<Chapter, ChapterListDTO>();
            CreateMap<ChapterText, ChapterTextDTO>()
            .ForMember(dest => dest.ChapterName, opt => opt.MapFrom(src => src.Chapter.Name)); // Map Chapter's Name to ChapterTextDTO's ChapterName
            CreateMap<Comment, CommentDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName)); // Map User's Name
            
        }

    }
}
