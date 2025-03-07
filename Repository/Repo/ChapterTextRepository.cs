using BussinessLayer;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class ChapterTextRepository : IChapterTextRepository
    {
        private readonly ChapterTextDAO _chapterTextDAO = ChapterTextDAO.Instance;

        public async Task<IEnumerable<ChapterText>> GetAll() => await _chapterTextDAO.GetAllChapterTexts();

        public async Task<ChapterText> GetById(int id) => await _chapterTextDAO.GetChapterTextById(id);

        public async Task Add(ChapterText chapterText) => await _chapterTextDAO.Add(chapterText);

        public async Task Update(ChapterText chapterText) => await _chapterTextDAO.Update(chapterText);

        public async Task Delete(int id) => await _chapterTextDAO.Delete(id);
    }
}
