using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class ReadingHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MangaId { get; set; }
        public int ChapterId { get; set; }
        public DateTime ReadDate { get; set; }
        public string Status { get; set; }

        public User User { get; set; }
        public Manga Manga { get; set; }
        public Chapter Chapter { get; set; }
    }

}
