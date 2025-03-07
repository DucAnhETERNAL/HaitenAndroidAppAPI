using BussinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GenreDAO : SingletonBase<GenreDAO>
    {
        public async Task<IEnumerable<Genres>> GetAllGenres()
        {
            return await _context.Genres.Include(g => g.Mangas).ToListAsync();
        }

        public async Task<Genres> GetGenreById(int id)
        {
            return await _context.Genres.Include(g => g.Mangas).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task Add(Genres genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Genres genre)
        {
            var existingGenre = await GetGenreById(genre.Id);
            if (existingGenre != null)
            {
                _context.Entry(existingGenre).CurrentValues.SetValues(genre);
            }
            else
            {
                _context.Genres.Add(genre);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var genre = await GetGenreById(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
        }
    }
}

