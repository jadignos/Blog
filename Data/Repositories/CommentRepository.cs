using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));
        public void AddComment(int postId, Comment comment)
        {
            comment.PostId = postId;
            _context.Comments.Add(comment);
        }

        public Comment GetComment(int id) =>
            _context.Comments
            .AsNoTracking()
            .FirstOrDefault(comment => comment.Id == id);

        public ICollection<Comment> GetComments(int postId) =>
            _context.Comments
            .AsNoTracking()
            .Where(comment => comment.PostId == postId)
            .ToList();

        public ICollection<Comment> GetCommentsDesc(int postId) =>
            _context.Comments
            .AsNoTracking()
            .Where(comment => comment.PostId == postId)
            .OrderByDescending(comment => comment.CreatedDate)
            .ToList();

        public void RemoveComment(int id) =>
            _context.Comments
            .Remove(GetComment(id));

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;

        public void UpdateComment(int postId, Comment comment)
        {
            comment.PostId = postId;
            _context.Comments.Update(comment);
        }
    }
}
