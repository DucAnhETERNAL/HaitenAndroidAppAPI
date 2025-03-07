﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }

        public ICollection<UserMangaList> UserMangaLists { get; set; }
        public ICollection<ReadingHistory> ReadingHistories { get; set; }
        public ICollection<Rate> Rates { get; set; }
    }

}
