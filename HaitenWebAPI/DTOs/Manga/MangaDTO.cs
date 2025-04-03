using BussinessLayer;
using HaitenWebAPI.DTOs.Chapter;
using HaitenWebAPI.DTOs.Comment;
using HaitenWebAPI.DTOs.Genres;



namespace HaitenWebAPI.DTOs.Manga
{
    public class MangaDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public int GenreId { get; set; }
        public string Status { get; set; }
        public List<ChapterListDTO> Chapters { get; set; }
        public List<GenresDTO> Genres { get; set; }
        public double AverageRating { get; set; }  




    }
}
