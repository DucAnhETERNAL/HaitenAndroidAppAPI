namespace HaitenWebAPI.DTOs.ReadingHistoryDT
{
    public class ReadingHistoryDTO
    {
        
        public int MangaId { get; set; }
        public int ChapterId { get; set; }
        public DateTime ReadDate { get; set; }
        public string Status { get; set; }
    }
}
