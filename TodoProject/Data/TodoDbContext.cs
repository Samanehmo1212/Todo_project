using Microsoft.EntityFrameworkCore;
using TodoProject.Models;

namespace TodoProject.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Todo> Todo { get; set; }
    }
}
