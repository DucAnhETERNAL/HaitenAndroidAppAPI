namespace HaitenWebAPI.DTOs.Rate
{
    public class AddRateRequest
    {
        public int UserId { get; set; }  // ID của người dùng
        public int MangaId { get; set; }  // ID của Manga được đánh giá
        public int Rating { get; set; }  // Số sao đánh giá (1-5)
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }  // Thời gian đánh giá

        public AddRateRequest()
        {
            CreatedAt = DateTime.UtcNow;  // Mặc định lấy thời gian hiện tại khi đánh giá
        }
    }
}
