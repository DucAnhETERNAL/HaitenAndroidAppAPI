using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class Chapter
    {
        public int Id { get; set; }
        public int MangaId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
        public int ViewCount { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Manga Manga { get; set; }
        public ChapterText ChapterText { get; set; }
        public ICollection<ChapterImages> ChapterImages { get; set; }
        public ICollection<ReadingHistory> ReadingHistories { get; set; }
    }


}
