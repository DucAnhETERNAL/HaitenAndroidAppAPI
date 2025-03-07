using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Manga
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public string ImageUrls { get; set; }
        public int GenreId { get; set; }
        public string Status { get; set; }
        public virtual Genres Genre { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
        public virtual ICollection<Rate> Rates { get; set; } = new List<Rate>();
        public virtual ICollection<UserMangaList> UserMangaLists { get; set; } = new List<UserMangaList>();
        public virtual ICollection<ReadingHistory> ReadingHistories { get; set; } = new List<ReadingHistory>(); // ✅ Thêm dòng này
    }



}
