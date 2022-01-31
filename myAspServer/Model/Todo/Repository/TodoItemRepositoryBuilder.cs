namespace myAspServer.Model.Todo.Repository
{
    using myAspServer.Context.Database;

    public static class TodoItemRepositoryBuilder
    {
        public static ITodoItemRepository Build(TodoDbContext dbContext)
        {
            TodoItemRepository todoItemRepository = new TodoItemRepository();
            todoItemRepository.DbContext = dbContext;
            return todoItemRepository;
        }
    }
}
