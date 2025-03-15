using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICommentRepository : IRepositoryBase<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsByChapterId(int chapterId);
     
        
        Task<IEnumerable<Comment>> GetCommentsByUserId(int userId);

    }
}
