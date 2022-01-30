namespace myAspServer.Context.Database
{
    using Microsoft.EntityFrameworkCore;
    using myAspServer.Model.Todo.Entity;
    
    public class TodoDb : DbContext
    {        
        public TodoDb(DbContextOptions<TodoDb> options) : base(options) { }

        public DbSet<TodoItemEntity> Todos => Set<TodoItemEntity>();
    }
}
