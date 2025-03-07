using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class Genres
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Manga> Mangas { get; set; }
    }

}
