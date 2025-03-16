namespace HaitenWebAPI.DTOs.UserMangaList
{
    public class AddUserMangaListRequest
    {
        public int UserId { get; set; }  // ID của người dùng
        public int MangaId { get; set; }  // ID của Manga
        public bool IsFavorite { get; set; }  // Đánh dấu manga yêu thích

        
    }
}
