namespace HaitenWebAPI.DTOs.UserMangaList
{
    public class UpdateFavoriteStatusRequest
    {
        public int UserId { get; set; }
        public int MangaId { get; set; }
        public bool IsFavorite { get; set; }
    }

}
