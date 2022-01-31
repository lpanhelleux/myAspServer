namespace myAspServer.Model.Todo.Service
{
    using myAspServer.Model.Todo.Repository;

    public static class TodoItemServiceBuilder
    {
        public static ITodoItemService Build(ITodoItemRepository todoItemRepository)
        {
            TodoItemService todoItemService = new TodoItemService();
            todoItemService.todoItemRepository = todoItemRepository;
            return todoItemService;
        }
    }
}
