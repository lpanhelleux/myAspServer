namespace myAspServer.Model.Todo.Repository
{
    using Microsoft.EntityFrameworkCore;
    using myAspServer.Context.Database;
    using myAspServer.Model.Common.Entity;
    using myAspServer.Model.Todo.Entity;

    public class TodoItemRepository : ITodoItemRepository
    {
        public TodoItemRepository(TodoDbContext dbContext)
        {
            DbContext = dbContext;
            DbContext.Database.EnsureCreated();
        }
        private TodoDbContext DbContext { get; }

        public async void Add(TodoItemEntity todoItem)
        {
            if (todoItem.UserId != null)
            {
                todoItem.User = DbContext.Users.Single(u => todoItem.UserId == u.Id);
            }

            DbContext.Todos.Add(todoItem);
            await DbContext.SaveChangesAsync();
        }

        public async Task<ITodoResult> Delete(int id)
        {
            if (await DbContext.Todos.FindAsync(id) is TodoItemEntity todo)
            {
                DbContext.Todos.Remove(todo);
                await DbContext.SaveChangesAsync();
                return TodoResults.Ok(todo);
            }

            return TodoResults.NotFound();
        }

        public async Task<TodoItemEntity?> Get(int id)
        {
            if (await DbContext.Todos.FindAsync(id) is TodoItemEntity todo)
            {
                return todo;
            }

            return null;
        }

        public async Task<IList<TodoItemEntity>> GetAll()
        {
            return await DbContext.Todos.ToListAsync();
        }

        public async Task<IList<TodoItemEntity>> GetAllByUserId(int userId)
        {
            return await DbContext.Todos.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<ITodoResult> Update(int id, string? name, bool isComplete)
        {
            var todo = await DbContext.Todos.FindAsync(id);

            if (todo is null) return TodoResults.NotFound();

            todo.Name = name;
            todo.IsComplete = isComplete;

            await DbContext.SaveChangesAsync();

            return TodoResults.NoContent();
        }
    }
}
