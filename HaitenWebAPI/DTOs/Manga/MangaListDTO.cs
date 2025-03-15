namespace HaitenWebAPI.DTOs.Manga
{
    public class MangaListDTO
    {
        public int Id { get; set; }  // The ID of the manga
        public string Title { get; set; }  // The title of the manga
        public string GenreName { get; set; }  // The name of the genre (you can get this from the Genre entity)
        public double AverageRating { get; set; }  // The average rating from all users (calculated)
    }
}
