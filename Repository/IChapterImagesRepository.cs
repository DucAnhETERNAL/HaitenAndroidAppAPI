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
        Task<List<ChapterImages>> AddMultipleImagesAsync(List<ChapterImages> images);
        Task<List<ChapterImages>> GetImagesByChapterIdAsync(int chapterId);
        Task<bool> DeleteImageAsync(int id);
        Task AddChapterImageAsync(ChapterImages chapterImage);
        Task<Chapter> FindChapterByIdAsync(int chapterId);

    }
}
