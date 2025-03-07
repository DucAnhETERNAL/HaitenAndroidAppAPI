using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> Login(string username, string password);
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetUsersByRole(string role);
        Task<int> GetUserCount();
    }
}
