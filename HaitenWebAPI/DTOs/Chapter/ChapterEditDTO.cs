namespace HaitenWebAPI.DTOs.Chapter
{
    public class ChapterEditDTO{
        public string Name { get; set; } // Tên chương có thể thay đổi
        public bool Status { get; set; } // Trạng thái của chương (kích hoạt hay không)
        public string Content { get; set; } // Nội dung chương, có thể cập nhậ\
    }
}
