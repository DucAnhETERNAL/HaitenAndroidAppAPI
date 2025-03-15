namespace HaitenWebAPI.DTOs.Comment
{
    public class AddCommentRequest
    {
        public int UserId { get; set; }  // The ID of the user who is adding the comment
        public int ChapterId { get; set; }  // The ID of the manga the comment is for
        public string Content { get; set; }  // The content of the comment
        public DateTime CreatedAt { get; set; }  // The time when the comment was created

        public AddCommentRequest()
        {
            CreatedAt = DateTime.UtcNow;  // Default to the current time when the comment is created
        }
    }

}
