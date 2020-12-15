using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public void AddPost(Post post) =>
            _context.Posts.Add(post);

        public Post GetPost(int id) =>
            _context.Posts
            .AsNoTracking()
            .Include(post => post.Comments)
            .ThenInclude(comment => comment.SubComments)
            .FirstOrDefault(post => post.Id == id);

        public IEnumerable<Post> GetPosts() =>
            _context.Posts.AsNoTracking().ToList();

        public IQueryable<Post> GetPostsDescending() =>
            _context.Posts.AsNoTracking().OrderByDescending(post => post.DateCreated);

        public IEnumerable<Post> GetPostsByCategory(int categoryId) =>
            _context.Posts.AsNoTracking().Where(post => post.CategoryId == categoryId).ToList();

        public void RemovePost(int id) =>
            _context.Posts.Remove(GetPost(id));

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;

        public void UpdatePost(Post post) =>
            _context.Posts.Update(post);
    }
}
