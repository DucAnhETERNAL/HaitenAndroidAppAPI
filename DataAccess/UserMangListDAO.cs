using Microsoft.EntityFrameworkCore;
using BussinessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessObject;

namespace DataAccess
{
    public class UserMangaListDAO : SingletonBase<UserMangaListDAO>
    {
        public async Task<IEnumerable<UserMangaList>> GetAllUserMangaLists()
        {
            return await _context.UserMangaLists
                .Include(uml => uml.User)
                .Include(uml => uml.Manga)
                .ToListAsync();
        }

        public async Task<UserMangaList> GetUserMangaListById(int id)
        {
            return await _context.UserMangaLists
                .Include(uml => uml.User)
                .Include(uml => uml.Manga)
                .FirstOrDefaultAsync(uml => uml.Id == id);
        }

        public async Task Add(UserMangaList userMangaList)
        {
            _context.UserMangaLists.Add(userMangaList);
            await _context.SaveChangesAsync();
        }

        public async Task Update(UserMangaList userMangaList)
        {
            var existingUserMangaList = await GetUserMangaListById(userMangaList.Id);
            if (existingUserMangaList != null)
            {
                _context.Entry(existingUserMangaList).CurrentValues.SetValues(userMangaList);
            }
            else
            {
                _context.UserMangaLists.Add(userMangaList);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var userMangaList = await GetUserMangaListById(id);
            if (userMangaList != null)
            {
                _context.UserMangaLists.Remove(userMangaList);
                await _context.SaveChangesAsync();
            }
        }
    }
}
