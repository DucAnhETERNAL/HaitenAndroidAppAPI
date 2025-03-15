namespace HaitenWebAPI.DTOs.ReadingHistoryDT
{
    public class AddReadingHistoryRequest
    {
        public int UserId { get; set; }  // ID của người dùng
        public int MangaId { get; set; }  // ID của Manga
        public int ChapterId { get; set; }  // ID của chương đã đọc
        public string Status { get; set; }  // Trạng thái đọc (đã đọc, đang đọc, chưa đọc)
        public DateTime ReadDate { get; set; }  // Ngày đọc

        public AddReadingHistoryRequest()
        {
            ReadDate = DateTime.UtcNow;  // Mặc định lấy thời gian hiện tại khi thêm vào
            Status = "Đang đọc";  // Mặc định trạng thái là "Đang đọc"
        }
    }
}
