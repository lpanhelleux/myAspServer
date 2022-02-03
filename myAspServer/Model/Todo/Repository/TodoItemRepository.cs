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
            if (DbContext != null)
            {
                DbContext.Todos.Add(todoItem);
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task<IResult> Delete(int id)
        {
            if (DbContext != null)
            {
                if (await DbContext.Todos.FindAsync(id) is TodoItemEntity todo)
                {
                    DbContext.Todos.Remove(todo);
                    await DbContext.SaveChangesAsync();
                    return Results.Ok(todo);
                }
            }

            return Results.NotFound();
        }

        public async Task<TodoItemEntity?> Get(int id)
        {
            if (DbContext != null)
            {
                if (await DbContext.Todos.FindAsync(id) is TodoItemEntity todo)
                {
                    return todo;
                }
            }
            
            return null;
        }

        public async Task<IList<TodoItemEntity>> GetAll()
        {
            if (DbContext != null)
            {
                return await DbContext.Todos.ToListAsync();
            }

            return new List<TodoItemEntity>();
        }

        public async Task<IResult> Update(int id, string? name, bool isComplete)
        {
            if (DbContext != null)
            {
                var todo = await DbContext.Todos.FindAsync(id);

                if (todo is null) return Results.NotFound();

                todo.Name = name;
                todo.IsComplete = isComplete;

                await DbContext.SaveChangesAsync();

                return Results.NoContent();
            }

            return Results.StatusCode(500);
        }
    }
}
