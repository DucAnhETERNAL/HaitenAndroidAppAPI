using BussinessLayer;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO _userDAO = UserDAO.Instance;

        public async Task<IEnumerable<User>> GetAll() => await _userDAO.GetAllUsers();

        public async Task<User> GetById(int id) => await _userDAO.GetUserById(id);

        public async Task Add(User user) => await _userDAO.Add(user);

        public async Task Update(User user) => await _userDAO.Update(user);

        public async Task Delete(int id) => await _userDAO.Delete(id);

        public async Task<User> Login(string username, string password) => await _userDAO.Login(username, password);

        public async Task<User> GetUserByEmail(string email) => await _userDAO.GetUserByEmail(email);

        public async Task<IEnumerable<User>> GetUsersByRole(string role) => await _userDAO.GetUsersByRole(role);

        public async Task<int> GetUserCount() => await _userDAO.GetUserCount();
    }
}
