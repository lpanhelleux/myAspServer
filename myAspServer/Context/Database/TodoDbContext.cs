namespace myAspServer.Context.Database
{
    using Microsoft.EntityFrameworkCore;
    using myAspServer.Model.Todo.Entity;
    
    public class TodoDbContext : DbContext
    {        
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<TodoItemEntity> Todos => Set<TodoItemEntity>();
    }
}
