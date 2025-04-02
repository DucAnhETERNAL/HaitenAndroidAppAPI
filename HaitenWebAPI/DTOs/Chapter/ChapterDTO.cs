namespace HaitenWebAPI.DTOs.Chapter
{
    public class ChapterDTO
    {
        public int Id { get; set; }
        public int MangaId { get; set; }
        public string Name { get; set; }
        //public bool Status { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ChapterDTO> Chapters { get; set; }

    }
}