using Microsoft.EntityFrameworkCore;
using BussinessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessObject;

namespace DataAccess
{
    public class UserDAO :SingletonBase<UserDAO>
    {
       

        

        // Lấy danh sách tất cả người dùng
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // Lấy người dùng theo ID
        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        // Thêm người dùng mới
        public async Task Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin người dùng
        public async Task Update(User user)
        {
            var existingUser = await GetUserById(user.Id);
            if (existingUser != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(user);
            }
            else
            {
                _context.Users.Add(user);
            }
            await _context.SaveChangesAsync();
        }

        // Xóa người dùng theo ID
        public async Task Delete(int id)
        {
            var user = await GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // Kiểm tra đăng nhập
        public async Task<User> Login(string username, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }

        // Tìm người dùng bằng email
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Lấy danh sách người dùng theo vai trò
        public async Task<IEnumerable<User>> GetUsersByRole(string role)
        {
            return await _context.Users
                .Where(u => u.Role == role)
                .ToListAsync();
        }

        // Đếm số lượng người dùng
        public async Task<int> GetUserCount()
        {
            return await _context.Users.CountAsync();
        }
    }
}
