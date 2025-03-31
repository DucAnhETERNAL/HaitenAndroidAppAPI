using BussinessLayer;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class ChapterImagesRepository 
    {
        private readonly ChapterImagesDAO _chapterImagesDAO;

        public ChapterImagesRepository(ChapterImagesDAO chapterImagesDAO)
        {
            _chapterImagesDAO = chapterImagesDAO;
        }

        public async Task<List<ChapterImages>> AddMultipleImagesAsync(List<ChapterImages> images)
        {
            return await _chapterImagesDAO.AddMultipleAsync(images);
        }
    

        public async Task<List<ChapterImages>> GetImagesByChapterIdAsync(int chapterId)
        {
            return await _chapterImagesDAO.GetImagesByChapterIdAsync(chapterId);
        }

        public async Task<bool> DeleteImageAsync(int id)
        {
            return await _chapterImagesDAO.DeleteAsync(id);
        }
        public async Task AddChapterImageAsync(ChapterImages chapterImage)
        {
            await _chapterImagesDAO.AddChapterImageAsync(chapterImage);
        }   

        public async Task<Chapter> FindChapterByIdAsync(int chapterId)
        {
            return await _chapterImagesDAO.FindChapterByIdAsync(chapterId);
        }

    }
}
