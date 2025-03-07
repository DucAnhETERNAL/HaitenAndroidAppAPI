using BussinessLayer;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class RateRepository : IRateRepository
    {
        private readonly RateDAO _rateDAO = RateDAO.Instance;

        public async Task<IEnumerable<Rate>> GetAll() => await _rateDAO.GetAllRates();

        public async Task<Rate> GetById(int id) => await _rateDAO.GetRateById(id);

        public async Task Add(Rate rate) => await _rateDAO.Add(rate);

        public async Task Update(Rate rate) => await _rateDAO.Update(rate);

        public async Task Delete(int id) => await _rateDAO.Delete(id);
    }
}
