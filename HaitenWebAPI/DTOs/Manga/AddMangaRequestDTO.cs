namespace HaitenWebAPI.DTOs.Manga
{
    public class AddMangaRequestDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
       
        public int GenreId { get; set; }
        public string Status { get; set; }
    }
}
