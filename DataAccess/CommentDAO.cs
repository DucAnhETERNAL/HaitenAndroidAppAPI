using BussinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CommentDAO : SingletonBase<CommentDAO>
    {
        // Lấy tất cả các comment cho một chapter
        public async Task<IEnumerable<Comment>> GetCommentsByChapterId(int chapterId)
        {
            return await _context.Comments
                                 .Where(c => c.ChapterId == chapterId)
                                 .Include(c => c.User) // Bao gồm thông tin người dùng để biết ai đã bình luận
                                 .Include(c => c.Chapter) // Bao gồm thông tin chapter
                                 .ToListAsync();
        }

        // Lấy một comment theo ID
        public async Task<Comment> GetCommentById(int id)
        {
            return await _context.Comments
                                 .Include(c => c.User)
                                 .Include(c => c.Chapter)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Thêm một comment mới
        public async Task Add(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        // Cập nhật comment
        public async Task Update(Comment comment)
        {
            var existingComment = await GetCommentById(comment.Id);
            if (existingComment != null)
            {
                _context.Entry(existingComment).CurrentValues.SetValues(comment);
            }
            else
            {
                _context.Comments.Add(comment);
            }
            await _context.SaveChangesAsync();
        }

        // Xóa một comment
        public async Task Delete(int id)
        {
            var comment = await GetCommentById(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await _context.Comments
                                 .Include(c => c.User)  
                                 .Include(c => c.Chapter) // Bao gồm thông tin chapter
                                 .ToListAsync();
        }
        public async Task<IEnumerable<Comment>> GetCommentsByUserId(int userId)
        {
            return await _context.Comments
                                 .Where(c => c.UserId == userId) // Lọc theo UserId
                                 .Include(c => c.User)   // Bao gồm thông tin người dùng
                                 .Include(c => c.Chapter) // Bao gồm thông tin chapter
                                 .ToListAsync();
        }
    }
}
