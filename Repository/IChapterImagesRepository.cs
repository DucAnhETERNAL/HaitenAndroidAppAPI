using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IChapterImagesRepository
    {
        void AddChapterImage(ChapterImages chapterImage);
        Chapter FindChapterById(int chapterId);
        List<ChapterImages> GetImagesByChapterId(int chapterId);

        //    Task<IEnumerable<ChapterImages>> GetAll();  // Lấy tất cả ChapterImages
        //    Task<ChapterImages> GetById(int id);  // Lấy ChapterImage theo ID
        //    Task Add(ChapterImages chapterImage);  // Thêm ChapterImage mới
        //    Task Update(ChapterImages chapterImage);  // Cập nhật ChapterImage
        //    Task Delete(int id);  // Xóa ChapterImage
        //    Task<Chapter> FindChapterByIdAsync(int chapterId);  // Tìm Chapter theo ID
        //
    }
}
