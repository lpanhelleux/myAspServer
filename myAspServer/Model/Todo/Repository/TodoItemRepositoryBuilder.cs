namespace myAspServer.Model.Todo.Repository
{
    using myAspServer.Context.Database;

    public static class TodoItemRepositoryBuilder
    {
        public static ITodoItemRepository Build(TodoDbContext? dbContext)
        {
            TodoItemRepository todoItemRepository = new()
            {
                DbContext = dbContext
            };
            return todoItemRepository;
        }
    }
}
