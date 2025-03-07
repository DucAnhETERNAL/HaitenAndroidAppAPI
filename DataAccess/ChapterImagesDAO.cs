using Microsoft.EntityFrameworkCore;
using BussinessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessObject;

namespace DataAccess
{
    public class ChapterImagesDAO : SingletonBase<ChapterImagesDAO>
    {
        public async Task<IEnumerable<ChapterImages>> GetAllChapterImages()
        {
            return await _context.ChapterImages
                .Include(ci => ci.Chapter)
                .ToListAsync();
        }

        public async Task<ChapterImages> GetChapterImageById(int id)
        {
            return await _context.ChapterImages
                .Include(ci => ci.Chapter)
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task Add(ChapterImages chapterImage)
        {
            _context.ChapterImages.Add(chapterImage);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ChapterImages chapterImage)
        {
            var existingChapterImage = await GetChapterImageById(chapterImage.Id);
            if (existingChapterImage != null)
            {
                _context.Entry(existingChapterImage).CurrentValues.SetValues(chapterImage);
            }
            else
            {
                _context.ChapterImages.Add(chapterImage);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var chapterImage = await GetChapterImageById(id);
            if (chapterImage != null)
            {
                _context.ChapterImages.Remove(chapterImage);
                await _context.SaveChangesAsync();
            }
        }
    }
}
