namespace myAspServer.Controller.Todo
{
    using myAspServer.Model.Todo.Service;

    public static class TodoControllerBuilder
    {
        private static readonly TodoController? todoController;

        public static TodoController Build(ITodoItemService todoItemService)
        {
            return todoController ?? new TodoController(todoItemService);
        }
    }
}
