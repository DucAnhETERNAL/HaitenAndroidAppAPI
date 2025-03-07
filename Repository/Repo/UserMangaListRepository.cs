using BussinessLayer;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class UserMangaListRepository : IUserMangaListRepository
    {
        private readonly UserMangaListDAO _userMangaListDAO = UserMangaListDAO.Instance;

        public async Task<IEnumerable<UserMangaList>> GetAll() => await _userMangaListDAO.GetAllUserMangaLists();

        public async Task<UserMangaList> GetById(int id) => await _userMangaListDAO.GetUserMangaListById(id);

        public async Task Add(UserMangaList userMangaList) => await _userMangaListDAO.Add(userMangaList);

        public async Task Update(UserMangaList userMangaList) => await _userMangaListDAO.Update(userMangaList);

        public async Task Delete(int id) => await _userMangaListDAO.Delete(id);
    }
}
