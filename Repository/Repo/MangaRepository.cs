using BussinessLayer;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class MangaRepository : IMangaRepository
    {
        private readonly MangaDAO _mangaDAO = MangaDAO.Instance;

        public async Task<IEnumerable<Manga>> GetAll() => await _mangaDAO.GetAllMangas();

        public async Task<Manga> GetById(int id) => await _mangaDAO.GetMangaById(id);

        public async Task Add(Manga manga) => await _mangaDAO.Add(manga);

        public async Task Update(Manga manga) => await _mangaDAO.Update(manga);

        public async Task Delete(int id) => await _mangaDAO.Delete(id);
    }
}
