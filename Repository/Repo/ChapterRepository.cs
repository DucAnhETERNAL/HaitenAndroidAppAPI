using BussinessLayer;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly ChapterDAO _chapterDAO = ChapterDAO.Instance;

        public async Task<IEnumerable<Chapter>> GetAll() => await _chapterDAO.GetAllChapters();

        public async Task<Chapter> GetById(int id) => await _chapterDAO.GetChapterById(id);

        public async Task Add(Chapter chapter) => await _chapterDAO.Add(chapter);

        public async Task Update(Chapter chapter) => await _chapterDAO.Update(chapter);

        public async Task Delete(int id) => await _chapterDAO.Delete(id);
    }
}
