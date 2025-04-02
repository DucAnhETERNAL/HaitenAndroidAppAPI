using HaitenWebAPI.DTOs.Comment;

namespace HaitenWebAPI.DTOs.ChapterText
{
    public class ChapterTextDTO
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string Content { get; set; }
        public string ChapterName { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
