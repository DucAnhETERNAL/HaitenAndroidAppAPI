using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRateRepository : IRepositoryBase<Rate>
    {
        Task<IEnumerable<Rate>> GetByMangaId(int mangaId);

    }
}
