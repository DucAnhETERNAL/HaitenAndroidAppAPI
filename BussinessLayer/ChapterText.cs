using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class ChapterText
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string Content { get; set; }

        public Chapter Chapter { get; set; }
    }

}
