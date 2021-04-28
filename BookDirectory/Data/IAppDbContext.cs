using BookDirectory.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookDirectory.Data
{
    public interface IAppDbContext
    {
        DbSet<Book> Books { get; set; }
        Task<int> SaveChangesAsync();
    }
}