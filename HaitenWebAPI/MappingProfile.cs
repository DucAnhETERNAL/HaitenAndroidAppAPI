using AutoMapper;
using BussinessLayer;
using HaitenWebAPI.DTOs.Chapter;
using HaitenWebAPI.DTOs.ChapterText;
using HaitenWebAPI.DTOs.Comment;
using HaitenWebAPI.DTOs.Manga;
using HaitenWebAPI.DTOs.User;
using HaitenWebAPI.DTOs.UserMangaList;

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
            CreateMap<UserMangaList, UserMangaListDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.MangaId, opt => opt.MapFrom(src => src.MangaId))
                .ForMember(dest => dest.IsFavorite, opt => opt.MapFrom(src => src.IsFavorite))
                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => src.AddedAt));

            CreateMap<AddMangaRequestDTO, Manga>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
            CreateMap<MangaEditDTO, Manga>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }

    }
}
