using Microsoft.EntityFrameworkCore;
using BussinessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessObject;

namespace DataAccess
{
    public class RateDAO : SingletonBase<RateDAO>
    {
        public async Task<IEnumerable<Rate>> GetAllRates()
        {
            return await _context.Rates
                .Include(r => r.User)
                .Include(r => r.Manga)
                .ToListAsync();
        }

        public async Task<Rate> GetRateById(int id)
        {
            return await _context.Rates
                .Include(r => r.User)
                .Include(r => r.Manga)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Add(Rate rate)
        {
            _context.Rates.Add(rate);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Rate rate)
        {
            var existingRate = await GetRateById(rate.Id);
            if (existingRate != null)
            {
                _context.Entry(existingRate).CurrentValues.SetValues(rate);
            }
            else
            {
                _context.Rates.Add(rate);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var rate = await GetRateById(id);
            if (rate != null)
            {
                _context.Rates.Remove(rate);
                await _context.SaveChangesAsync();
            }
        }
    }
}
