namespace myAspServer.Model.Todo.Repository
{
    using Microsoft.EntityFrameworkCore;
    using myAspServer.Context.Database;
    using myAspServer.Model.Todo.Entity;

    public class TodoItemRepository : ITodoItemRepository
    {
        public TodoDbContext? DbContext { get; set; }

        public async void Add(TodoItemEntity todoItem)
        {
            if (DbContext == null)
            {
                return;
            }

            DbContext.Todos.Add(todoItem);
            await DbContext.SaveChangesAsync();
        }

        public async Task<ITodoItemResult> Delete(int id)
        {
            if (DbContext == null)
            {
                return TodoItemResults.NotFound();
            }

            if (await DbContext.Todos.FindAsync(id) is TodoItemEntity todo)
            {
                DbContext.Todos.Remove(todo);
                await DbContext.SaveChangesAsync();
                return TodoItemResults.Ok(todo);
            }

            return TodoItemResults.NotFound();
        }

        public async Task<TodoItemEntity?> Get(int id)
        {
            if (DbContext == null)
            {
                return null;
            }

            if (await DbContext.Todos.FindAsync(id) is TodoItemEntity todo)
            {
                return todo;
            }

            return null;
        }

        public async Task<IList<TodoItemEntity>> GetAll()
        {
            return DbContext != null ? await DbContext.Todos.ToListAsync() : new List<TodoItemEntity>();
        }

        public async Task<ITodoItemResult> Update(int id, string? name, bool isComplete)
        {
            if (DbContext == null)
            {
                throw new Exception();
            }

            var todo = await DbContext.Todos.FindAsync(id);

            if (todo is null) return TodoItemResults.NotFound();

            todo.Name = name;
            todo.IsComplete = isComplete;

            await DbContext.SaveChangesAsync();

            return TodoItemResults.NoContent();
        }
    }
}
