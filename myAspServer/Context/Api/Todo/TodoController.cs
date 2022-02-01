namespace myAspServer.Context.Api.Todo
{
    using myAspServer.Model.Todo.Entity;
    using Microsoft.EntityFrameworkCore;
    using System.Text.Json;
    using myAspServer.Context.Database;
    using myAspServer.Model.Todo.Service;
    using myAspServer.Model.Todo.Repository;

    public static class TodoController
    {
        public static void Init(WebApplication app)
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

            app.MapGet("/todoitems", (TodoDbContext db) =>
            {
                ITodoItemRepository todoItemRepository = TodoItemRepositoryBuilder.Build(db);
                ITodoItemService todoItemService = TodoItemServiceBuilder.Build(todoItemRepository);

                return Results.Ok(todoItemService.GetAll().Select(x => new TodoItemDTO(x)).ToList());
            });

            app.MapGet("/todoitems/{id}", (int id, TodoDbContext db) =>
            {
                ITodoItemRepository todoItemRepository = TodoItemRepositoryBuilder.Build(db);
                ITodoItemService todoItemService = TodoItemServiceBuilder.Build(todoItemRepository);

                if (todoItemService.Get(id) is TodoItemEntity todoItemEntity)
                {
                    return Results.Ok(new TodoItemDTO(todoItemEntity));
                }
                else
                {
                    return Results.NotFound();
                }
            });

            app.MapPost("/todoitems", (TodoItemDTO todoItemDTO, TodoDbContext db) =>
            {
                TodoItemEntity todo = new()
                {
                    Id = todoItemDTO.Id,
                    Name = todoItemDTO.Name,
                    IsComplete = todoItemDTO.IsComplete,
                };

                ITodoItemRepository todoItemRepository = TodoItemRepositoryBuilder.Build(db);
                ITodoItemService todoItemService = TodoItemServiceBuilder.Build(todoItemRepository);
                todoItemService.Add(todo);

                return Results.Created($"/todoitems/{todo.Id}", new TodoItemDTO(todo));
            });

            app.MapPut("/todoitems/{id}", async (int id, TodoItemDTO inputTodoItemDTO, TodoDbContext db) =>
            {
                var todo = await db.Todos.FindAsync(id);

                if (todo is null) return Results.NotFound();

                todo.Name = inputTodoItemDTO.Name;
                todo.IsComplete = inputTodoItemDTO.IsComplete;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapDelete("/todoitems/{id}", (int id, TodoDbContext db) =>
            {
                ITodoItemRepository todoItemRepository = TodoItemRepositoryBuilder.Build(db);
                ITodoItemService todoItemService = TodoItemServiceBuilder.Build(todoItemRepository);
                todoItemService.Delete(id);
            });
        }
    }
}
