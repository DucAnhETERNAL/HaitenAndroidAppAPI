using BussinessLayer;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class ReadingHistoryRepository : IReadingHistoryRepository
    {
        private readonly ReadingHistoryDAO _readingHistoryDAO = ReadingHistoryDAO.Instance;

        public async Task<IEnumerable<ReadingHistory>> GetAll() => await _readingHistoryDAO.GetAllHistories();

        public async Task<ReadingHistory> GetById(int id) => await _readingHistoryDAO.GetHistoryById(id);

        public async Task Add(ReadingHistory history) => await _readingHistoryDAO.Add(history);

        public async Task Update(ReadingHistory history) => await _readingHistoryDAO.Update(history);

        public async Task Delete(int id) => await _readingHistoryDAO.Delete(id);
    }
}

