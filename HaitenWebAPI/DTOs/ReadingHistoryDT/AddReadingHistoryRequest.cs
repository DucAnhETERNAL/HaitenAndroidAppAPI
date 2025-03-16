namespace HaitenWebAPI.DTOs.ReadingHistoryDT
{
    public class AddReadingHistoryRequest
    {
        
        public int UserId { get; set; }  // ID của người dùng
        public int MangaId { get; set; }  // ID của Manga
        public int ChapterId { get; set; }  // ID của chương đã đọc
       
        public DateTime ReadDate { get; set; }  // Ngày đọc

       
    }
}
