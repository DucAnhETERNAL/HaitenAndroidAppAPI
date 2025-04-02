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
        // Khởi tạo đối tượng DAO thông qua Singleton
        private readonly ChapterImagesDAO _chapterImagesDAO;

        public ChapterImagesRepository(ChapterImagesDAO chapterImagesDAO)
        {
            _chapterImagesDAO = chapterImagesDAO;
        }
        public void AddChapterImage(ChapterImages chapterImage)
        {
            _chapterImagesDAO.AddChapterImage(chapterImage);
        }

        public Chapter FindChapterById(int chapterId)
        {
            return _chapterImagesDAO.FindChapterById(chapterId);
        }
        public List<ChapterImages> GetImagesByChapterId(int chapterId)
        {
            return _chapterImagesDAO.GetImagesByChapterId(chapterId);
        }
        public ChapterImages GetChapterImageById(int id)
        {
            return _chapterImagesDAO.GetChapterImageById(id);
        }

        public void UpdateChapterImage(ChapterImages chapterImage)
        {
            _chapterImagesDAO.UpdateChapterImage(chapterImage);
        }

        public void DeleteChapterImage(int id)
        {
            _chapterImagesDAO.DeleteChapterImage(id);
        }

    }
}
