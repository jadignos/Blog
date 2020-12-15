using Blog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Repositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPosts();
        IQueryable<Post> GetPostsDescending();
        IEnumerable<Post> GetPostsByCategory(int categoryId);
        Post GetPost(int id);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(int id);
        Task<bool> SaveChangesAsync();
    }
}
