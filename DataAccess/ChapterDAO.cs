using Microsoft.EntityFrameworkCore;
using BussinessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessObject;

namespace DataAccess
{
    public class ChapterDAO : SingletonBase<ChapterDAO>
    {
        public async Task<IEnumerable<Chapter>> GetAllChapters()
        {
            return await _context.Chapters
                .Include(c => c.Manga)
                .Include(c => c.ChapterText)
                .Include(c => c.ChapterImages)
                .Include(c => c.ReadingHistories)
                .ToListAsync();
        }

        public async Task<Chapter> GetChapterById(int id)
        {
            return await _context.Chapters
                .Include(c => c.Manga)
                .Include(c => c.ChapterText)
                .Include(c => c.ChapterImages)
                .Include(c => c.ReadingHistories)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Add(Chapter chapter)
        {
            _context.Chapters.Add(chapter);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Chapter chapter)
        {
            var existingChapter = await GetChapterById(chapter.Id);
            if (existingChapter != null)
            {
                _context.Entry(existingChapter).CurrentValues.SetValues(chapter);
            }
            else
            {
                _context.Chapters.Add(chapter);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var chapter = await GetChapterById(id);
            if (chapter != null)
            {
                _context.Chapters.Remove(chapter);
                await _context.SaveChangesAsync();
            }
        }
    }
}
