namespace HaitenWebAPI.DTOs.Chapter
{
    public class AddChapterRequestDTO
    {
        public int MangaId { get; set; } // The ID of the manga the chapter belongs to
        public string Name { get; set; } // The name of the chapter
        public bool Status { get; set; } = true; // The status of the chapter (active/inactive)
        public string Content { get; set; } // The content of the chapter (this will be linked to ChapterText)

    }
}
