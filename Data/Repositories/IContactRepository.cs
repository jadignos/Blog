using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Repositories
{
    public interface IContactRepository
    {
        void AddContact(Contact contact);

        Task<bool> SaveChangesAsync();
    }
}
