using BussinessLayer;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class ChapterImagesRepository : IChapterImagesRepository
    {
        private readonly ChapterImagesDAO _chapterImagesDAO = ChapterImagesDAO.Instance;

        public async Task<IEnumerable<ChapterImages>> GetAll() => await _chapterImagesDAO.GetAllChapterImages();

        public async Task<ChapterImages> GetById(int id) => await _chapterImagesDAO.GetChapterImageById(id);

        public async Task Add(ChapterImages chapterImage) => await _chapterImagesDAO.Add(chapterImage);

        public async Task Update(ChapterImages chapterImage) => await _chapterImagesDAO.Update(chapterImage);

        public async Task Delete(int id) => await _chapterImagesDAO.Delete(id);
    }
}
