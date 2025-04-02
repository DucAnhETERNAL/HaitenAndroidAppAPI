using Microsoft.EntityFrameworkCore;
using BussinessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessObject;

namespace DataAccess
{
    public class ChapterImagesDAO
    {
        private readonly PRMDbContext _dbContext;

        public ChapterImagesDAO(PRMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddChapterImage(ChapterImages chapterImage)
        {
            _dbContext.ChapterImages.Add(chapterImage);
            _dbContext.SaveChanges();
        }

        public Chapter FindChapterById(int chapterId)
        {
            return _dbContext.Chapters.Find(chapterId);
        }
        public List<ChapterImages> GetImagesByChapterId(int chapterId) // ✅ Thêm phương thức mới
        {
            return _dbContext.ChapterImages
                .Where(ci => ci.ChapterId == chapterId)
                .OrderBy(ci => ci.Position) // Sắp xếp theo Position từ thấp đến cao
                .ToList();
        }

    }
}
