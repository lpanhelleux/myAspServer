namespace myAspServer.Model.Todo.Service
{
    using myAspServer.Model.Todo.Repository;

    public static class TodoItemServiceBuilder
    {
        public static ITodoItemService Build(ITodoItemRepository todoItemRepository)
        {
            return new TodoItemService(todoItemRepository);
        }
    }
}
