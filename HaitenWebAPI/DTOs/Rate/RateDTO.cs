namespace HaitenWebAPI.DTOs.Rate
{
    public class RateDTO
    {
        public int Id { get; set; }
        public int MangaId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
