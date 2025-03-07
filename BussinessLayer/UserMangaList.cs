using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class UserMangaList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MangaId { get; set; }
        public DateTime AddedAt { get; set; }
        public bool IsFavorite { get; set; }

        public User User { get; set; }
        public Manga Manga { get; set; }
    }

}
