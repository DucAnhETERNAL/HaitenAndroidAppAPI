namespace HaitenWebAPI.DTOs.ChapterText
{
    public class AddChapterTextRequest
    {
        public int ChapterId { get; set; }  // ID của Chapter cần thêm nội dung
        public string Content { get; set; }  // Nội dung văn bản của chương

        public AddChapterTextRequest()
        {
            Content = string.Empty;  // Mặc định nội dung là rỗng nếu chưa nhập
        }
    }
}
