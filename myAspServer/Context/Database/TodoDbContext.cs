namespace myAspServer.Context.Database
{
    using Microsoft.EntityFrameworkCore;
    using myAspServer.Model.Todo.Entity;
    using myAspServer.Model.User.Entity;

    public class TodoDbContext : DbContext
    {        
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<TodoItemEntity> Todos => Set<TodoItemEntity>();

        public DbSet<UserEntity> Users => Set<UserEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItemEntity>()
                .HasOne(t => t.User)
                .WithMany(u => u.TodoItems)
                .IsRequired(false);
        }
    }
}
