namespace HaitenWebAPI.DTOs.UserMangaList
{
    public class UserMangaListDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MangaId { get; set; }
        public DateTime AddedAt { get; set; }
        public bool IsFavorite { get; set; }
    }
}
