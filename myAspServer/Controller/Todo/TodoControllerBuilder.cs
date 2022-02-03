namespace myAspServer.Controller.Todo
{
    using myAspServer.Model.Todo.Service;

    public static class TodoControllerBuilder
    {
        public static TodoController Build(ITodoItemService todoItemService)
        {
            return new TodoController(todoItemService);
        }
    }
}
