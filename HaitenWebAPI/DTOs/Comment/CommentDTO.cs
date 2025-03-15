namespace HaitenWebAPI.DTOs.Comment
{
    public class CommentDTO
    {
        public int Id { get; set; }  // The ID of the comment
       
        public int UserId { get; set; }  // The ID of the user who made the comment
        public string Content { get; set; }  // The content of the comment
        public DateTime CreatedAt { get; set; }  // The time the comment was created
        public string UserName { get; set; }  // The name of the user who commented (optional)
        
    }
}
