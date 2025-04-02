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

        //// Lấy tất cả ChapterImages
        //public async Task<IEnumerable<ChapterImages>> GetAll()
        //{
        //    return await _chapterImagesDAO.GetAllChapterImages();
        //}

        //// Lấy ChapterImage theo ID
        //public async Task<ChapterImages> GetById(int id)
        //{
        //    return await _chapterImagesDAO.GetChapterImageById(id);
        //}

        //// Thêm ChapterImage mới
        //public async Task Add(ChapterImages chapterImage)
        //{
        //    await _chapterImagesDAO.AddAsync(chapterImage);  // Gọi phương thức AddAsync của DAO
        //}

        //// Cập nhật ChapterImage
        //public async Task Update(ChapterImages chapterImage)
        //{
        //    await _chapterImagesDAO.UpdateAsync(chapterImage);  // Gọi phương thức UpdateAsync của DAO
        //}

        //// Xóa ChapterImage theo ID
        //public async Task Delete(int id)
        //{
        //    await _chapterImagesDAO.DeleteAsync(id);  // Gọi phương thức DeleteAsync của DAO
        //}

        //// Tìm Chapter theo ChapterId
        //public async Task<Chapter> FindChapterByIdAsync(int chapterId)
        //{
        //    return await _chapterImagesDAO.FindChapterByIdAsync(chapterId);  // Gọi phương thức FindChapterByIdAsync của DAO
        //}
    }
}
