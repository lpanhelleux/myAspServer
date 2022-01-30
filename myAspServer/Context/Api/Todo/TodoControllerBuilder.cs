namespace myAspServer.Context.Api.Todo
{
    using myAspServer.Model.Todo.Entity;
    using Microsoft.EntityFrameworkCore;
    using System.Text.Json;
    using myAspServer.Context.Database;

    public static class TodoControllerBuilder
    {
        public static void Init(WebApplication app)
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

            app.MapGet("/todoitems", async (TodoDb db) =>
                await db.Todos.Select(x => new TodoItemDTO(x)).ToListAsync());

            app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
                await db.Todos.FindAsync(id)
                    is TodoItemEntity todo
                        ? Results.Ok(new TodoItemDTO(todo))
                        : Results.NotFound());

            app.MapPost("/todoitems", async (TodoItemDTO todoItemDTO, TodoDb db) =>
            {
                TodoItemEntity todo = new TodoItemEntity
                {
                    Id = todoItemDTO.Id,
                    Name = todoItemDTO.Name,
                    IsComplete = todoItemDTO.IsComplete,
                };

                db.Todos.Add(todo);
                await db.SaveChangesAsync();

                return Results.Created($"/todoitems/{todo.Id}", new TodoItemDTO(todo));
            });

            app.MapPut("/todoitems/{id}", async (int id, TodoItemDTO inputTodoItemDTO, TodoDb db) =>
            {
                var todo = await db.Todos.FindAsync(id);

                if (todo is null) return Results.NotFound();

                todo.Name = inputTodoItemDTO.Name;
                todo.IsComplete = inputTodoItemDTO.IsComplete;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
            {
                if (await db.Todos.FindAsync(id) is TodoItemEntity todo)
                {
                    db.Todos.Remove(todo);
                    await db.SaveChangesAsync();
                    return Results.Ok(todo);
                }

                return Results.NotFound();
            });
        }
    }
}
