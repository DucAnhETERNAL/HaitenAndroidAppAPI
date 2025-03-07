using Microsoft.EntityFrameworkCore;
using BussinessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessObject;

namespace DataAccess
{
    public class ChapterTextDAO : SingletonBase<ChapterTextDAO>
    {
        public async Task<IEnumerable<ChapterText>> GetAllChapterTexts()
        {
            return await _context.ChapterTexts
                .Include(ct => ct.Chapter)
                .ToListAsync();
        }

        public async Task<ChapterText> GetChapterTextById(int id)
        {
            return await _context.ChapterTexts
                .Include(ct => ct.Chapter)
                .FirstOrDefaultAsync(ct => ct.Id == id);
        }

        public async Task Add(ChapterText chapterText)
        {
            _context.ChapterTexts.Add(chapterText);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ChapterText chapterText)
        {
            var existingChapterText = await GetChapterTextById(chapterText.Id);
            if (existingChapterText != null)
            {
                _context.Entry(existingChapterText).CurrentValues.SetValues(chapterText);
            }
            else
            {
                _context.ChapterTexts.Add(chapterText);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var chapterText = await GetChapterTextById(id);
            if (chapterText != null)
            {
                _context.ChapterTexts.Remove(chapterText);
                await _context.SaveChangesAsync();
            }
        }
    }
}
