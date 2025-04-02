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
        ChapterImages GetChapterImageById(int id);
        void UpdateChapterImage(ChapterImages chapterImage);
        void DeleteChapterImage(int id);

    }
}
