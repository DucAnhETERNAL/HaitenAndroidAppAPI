using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class ChapterImages
    {
        [Key]
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string ImageUrl { get; set; }
        public int Position { get; set; }

        [ForeignKey("ChapterId")]
        public virtual Chapter Chapter { get; set; }

    }

}
