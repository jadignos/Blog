using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public ICollection<Category> GetCategories() =>
            _context.Categories
            .AsNoTracking()
            .ToList();

        public Category GetCategory(int id) =>
          _context.Categories
            .AsNoTracking()
            .FirstOrDefault(category => category.Id == id);
    }
}
