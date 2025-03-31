using Microsoft.EntityFrameworkCore;
using BussinessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessObject;

namespace DataAccess
{
    public class ChapterImagesDAO 
    {
        private readonly PRMDbContext _context;

        public ChapterImagesDAO(PRMDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChapterImages>> AddMultipleAsync(List<ChapterImages> images)
        {
            await _context.ChapterImages.AddRangeAsync(images);
            await _context.SaveChangesAsync();
            return images;
        }
        public async Task AddChapterImageAsync(ChapterImages chapterImage)
        {
            await _context.ChapterImages.AddAsync(chapterImage);
            await _context.SaveChangesAsync();
        }

        public async Task<Chapter> FindChapterByIdAsync(int chapterId)
        {
            return await _context.Chapters.FindAsync(chapterId);
        }


        public async Task<ChapterImages> GetByIdAsync(int id)
        {
            return await _context.ChapterImages.FindAsync(id);
        }

        public async Task<List<ChapterImages>> GetAllAsync()
        {
            return await _context.ChapterImages.ToListAsync();
        }

        public async Task<List<ChapterImages>> GetImagesByChapterIdAsync(int chapterId)
        {
            return await _context.ChapterImages.Where(ci => ci.ChapterId == chapterId).ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var image = await _context.ChapterImages.FindAsync(id);
            if (image != null)
            {
                _context.ChapterImages.Remove(image);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
