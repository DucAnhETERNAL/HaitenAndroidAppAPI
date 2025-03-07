using Microsoft.EntityFrameworkCore;
using BussinessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessObject;

namespace DataAccess
{
    public class ReadingHistoryDAO : SingletonBase<ReadingHistoryDAO>
    {
        public async Task<IEnumerable<ReadingHistory>> GetAllHistories()
        {
            return await _context.ReadingHistories
                .Include(r => r.User)
                .Include(r => r.Manga)
                .Include(r => r.Chapter)
                .ToListAsync();
        }

        public async Task<ReadingHistory> GetHistoryById(int id)
        {
            return await _context.ReadingHistories
                .Include(r => r.User)
                .Include(r => r.Manga)
                .Include(r => r.Chapter)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Add(ReadingHistory history)
        {
            _context.ReadingHistories.Add(history);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ReadingHistory history)
        {
            var existingHistory = await GetHistoryById(history.Id);
            if (existingHistory != null)
            {
                _context.Entry(existingHistory).CurrentValues.SetValues(history);
            }
            else
            {
                _context.ReadingHistories.Add(history);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var history = await GetHistoryById(id);
            if (history != null)
            {
                _context.ReadingHistories.Remove(history);
                await _context.SaveChangesAsync();
            }
        }
    }
}
