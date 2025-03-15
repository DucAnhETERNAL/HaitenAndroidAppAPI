using BussinessLayer;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CommentDAO _commentDAO = CommentDAO.Instance;

        // Lấy tất cả các comment cho một chapter
        public async Task<IEnumerable<Comment>> GetCommentsByChapterId(int chapterId)
            => await _commentDAO.GetCommentsByChapterId(chapterId);

        // Lấy comment theo ID
        public async Task<Comment> GetById(int id)
            => await _commentDAO.GetCommentById(id);

        // Thêm comment
        public async Task Add(Comment comment)
            => await _commentDAO.Add(comment);

        // Cập nhật comment
        public async Task Update(Comment comment)
            => await _commentDAO.Update(comment);

        // Xóa comment
        public async Task Delete(int id)
            => await _commentDAO.Delete(id);





        public async Task<IEnumerable<Comment>> GetCommentsByUserId(int userId)
            => await _commentDAO.GetCommentsByUserId(userId);

        public async Task<IEnumerable<Comment>> GetAll()
        => await _commentDAO.GetAllComments();
    }
}
