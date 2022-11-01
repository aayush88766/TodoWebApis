using Microsoft.EntityFrameworkCore;
using TodoListWebAPIs.Model;

namespace TodoListWebAPIs.DataAccessLayers
{
    public class TodoDBContext : DbContext
    {
        public TodoDBContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Todo> Todos { get; set; }
    }
}
