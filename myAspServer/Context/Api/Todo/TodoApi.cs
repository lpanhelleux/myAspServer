namespace myAspServer.Context.Api.Todo
{
    using myAspServer.Context.Database;
    using myAspServer.Controller.Todo;
    using myAspServer.Model.Todo.Repository;
    using myAspServer.Model.Todo.Service;
    using System.Text.Json;

    public class TodoApi
    {
        public void Init(WebApplication app)
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

            app.MapGet("/todoitems", (TodoDbContext dbContext) =>
            {
                TodoController todoController = BuildController(dbContext);
                return todoController.GetAll();
            });

            app.MapGet("/todoitems/{id}", (int id, TodoDbContext dbContext) =>
            {
                TodoController todoController = BuildController(dbContext);
                return todoController.Get(id);
            });

            app.MapPost("/todoitems", (TodoItemDTO todoItemDTO, TodoDbContext dbContext) =>
            {
                TodoController todoController = BuildController(dbContext);
                return todoController.Post(todoItemDTO);
            });

            app.MapPut("/todoitems/{id}", (int id, TodoItemDTO inputTodoItemDTO, TodoDbContext dbContext) =>
            {
                TodoController todoController = BuildController(dbContext);
                todoController.Put(id, inputTodoItemDTO);
            });

            app.MapDelete("/todoitems/{id}", (int id, TodoDbContext dbContext) =>
            {
                TodoController todoController = BuildController(dbContext);
                todoController.Delete(id);
            });
        }

        private static TodoController BuildController(TodoDbContext dbContext)
        {
            ITodoItemRepository todoItemRepository = TodoItemRepositoryBuilder.Build(dbContext);
            ITodoItemService todoItemService = TodoItemServiceBuilder.Build(todoItemRepository);
            TodoController todoController = TodoControllerBuilder.Build(todoItemService);
            return todoController;
        }
    }
}
