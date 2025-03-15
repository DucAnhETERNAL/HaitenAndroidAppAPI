namespace HaitenWebAPI.DTOs.ChapterImages
{
    public class ChapterImagesDTO
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string ImageUrl { get; set; }
        public int Position { get; set; }
    }
}
