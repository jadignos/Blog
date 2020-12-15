using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Repositories
{
    public class SubCommentRepository : ISubCommentRepository
    {
        private readonly AppDbContext _context;

        public SubCommentRepository(AppDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public void AddSubComment(int commentId, SubComment subComment)
        {
            subComment.CommentId = commentId;
            _context.SubComments.Add(subComment);
        }

        public SubComment GetSubComment(int id) =>
            _context.SubComments
            .AsNoTracking()
            .FirstOrDefault(subComment => subComment.Id == id);

        public IEnumerable<SubComment> GetSubComments(int commentId) =>
            _context.SubComments
            .AsNoTracking()
            .Where(subComment => subComment.CommentId == commentId)
            .OrderByDescending(subComment => subComment.CreatedDate)
            .ToList();

        public IEnumerable<SubComment> GetSubCommentsDescending(int commentId) =>
            _context.SubComments
            .AsNoTracking()
            .Where(subComment => subComment.CommentId == commentId)
            .OrderByDescending(subComment => subComment.CreatedDate)
            .ToList();

        public void RemoveSubComment(int id) =>
            _context.SubComments.Remove(GetSubComment(id));

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;

        public void UpdateSubComment(SubComment subComment) =>
            _context.SubComments.Update(subComment);
    }
}
