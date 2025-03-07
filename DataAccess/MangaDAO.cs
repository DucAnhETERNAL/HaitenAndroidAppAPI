using Microsoft.EntityFrameworkCore;
using BussinessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessObject;

namespace DataAccess
{
    public class MangaDAO : SingletonBase<MangaDAO>
    {
        public async Task<IEnumerable<Manga>> GetAllMangas()
        {
            return await _context.Mangas
                .Include(m => m.Genre)
                .Include(m => m.Chapters)
                .Include(m => m.Rates)
                .Include(m => m.UserMangaLists)
                .Include(m => m.ReadingHistories)
                .ToListAsync();
        }

        public async Task<Manga> GetMangaById(int id)
        {
            return await _context.Mangas
                .Include(m => m.Genre)
                .Include(m => m.Chapters)
                .Include(m => m.Rates)
                .Include(m => m.UserMangaLists)
                .Include(m => m.ReadingHistories)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Add(Manga manga)
        {
            _context.Mangas.Add(manga);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Manga manga)
        {
            var existingManga = await GetMangaById(manga.Id);
            if (existingManga != null)
            {
                _context.Entry(existingManga).CurrentValues.SetValues(manga);
            }
            else
            {
                _context.Mangas.Add(manga);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var manga = await GetMangaById(id);
            if (manga != null)
            {
                _context.Mangas.Remove(manga);
                await _context.SaveChangesAsync();
            }
        }
    }
}
