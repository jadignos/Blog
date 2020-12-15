using Blog.Models;
using System.Collections.Generic;

namespace Blog.Data.Repositories
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
    }
}
