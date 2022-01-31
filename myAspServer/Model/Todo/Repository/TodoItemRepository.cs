namespace myAspServer.Model.Todo.Repository
{
    using myAspServer.Context.Database;
    using myAspServer.Model.Todo.Entity;

    public class TodoItemRepository : ITodoItemRepository
    {
        public TodoDbContext? DbContext { get; set; }

        public async void Add(TodoItemEntity todoItem)
        {
            DbContext?.Todos.Add(todoItem);
            if (DbContext != null)
            {
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

        public void Get(TodoItemEntity todoItem)
        {
            throw new NotImplementedException();
        }

        public IList<TodoItemEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(TodoItemEntity todoItem)
        {
            throw new NotImplementedException();
        }
    }
}
