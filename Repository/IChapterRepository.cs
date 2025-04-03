using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IChapterRepository : IRepositoryBase<Chapter>
    {
        Task<IEnumerable<Chapter>> GetByMangaId(int mangaId);

    }
}
