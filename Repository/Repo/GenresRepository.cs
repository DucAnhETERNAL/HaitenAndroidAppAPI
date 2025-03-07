using BussinessLayer;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class GenresRepository : IGenresRepository
    {
        private readonly GenreDAO _genresDAO = GenreDAO.Instance;

        public async Task<IEnumerable<Genres>> GetAll() => await _genresDAO.GetAllGenres();

        public async Task<Genres> GetById(int id) => await _genresDAO.GetGenreById(id);

        public async Task Add(Genres genre) => await _genresDAO.Add(genre);

        public async Task Update(Genres genre) => await _genresDAO.Update(genre);

        public async Task Delete(int id) => await _genresDAO.Delete(id);
    }
}
