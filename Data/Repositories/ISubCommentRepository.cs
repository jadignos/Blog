using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Repositories
{
    public interface ISubCommentRepository
    {
        IEnumerable<SubComment> GetSubComments(int commentId);
        IEnumerable<SubComment> GetSubCommentsDescending(int commentId);
        SubComment GetSubComment(int id);
        void AddSubComment(int commentId, SubComment subComment);
        void UpdateSubComment(SubComment subComment);
        void RemoveSubComment(int id);
        Task<bool> SaveChangesAsync();
    }
}
