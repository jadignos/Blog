using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Repositories
{
    public interface ICommentRepository
    {
        ICollection<Comment> GetComments(int postId);
        ICollection<Comment> GetCommentsDesc(int postId);
        Comment GetComment(int id);
        void AddComment(int postId, Comment comment);
        void UpdateComment(int postId, Comment comment);
        void RemoveComment(int id);
        Task<bool> SaveChangesAsync();
    }
}
