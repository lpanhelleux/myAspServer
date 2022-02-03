namespace myAspServer.Controller.Todo
{
    using myAspServer.Model.Todo.Entity;
    using myAspServer.Model.Todo.Service;

    public class TodoController : ITodoController
    {
        private readonly ITodoItemService todoItemService;

        public TodoController(ITodoItemService todoItemService)
        {
            this.todoItemService = todoItemService;
        }

        public IResult GetAll()
        {
            return Results.Ok(todoItemService.GetAll().Select(x => new TodoItemDTO(x)).ToList());
        }

        public IResult Get(int id)
        {
            return todoItemService.Get(id) is TodoItemEntity todoItemEntity
                ? Results.Ok(new TodoItemDTO(todoItemEntity))
                : Results.NotFound();
        }

        public IResult Post(TodoItemDTO todoItemDTO)
        {
            TodoItemEntity todo = new()
            {
                Id = todoItemDTO.Id,
                Name = todoItemDTO.Name,
                IsComplete = todoItemDTO.IsComplete,
            };

            todoItemService.Create(todo);

            return Results.Created($"/todoitems/{todo.Id}", new TodoItemDTO(todo));
        }

        public IResult Put(int id, TodoItemDTO inputTodoItemDTO)
        {
            return todoItemService.Update(id, inputTodoItemDTO.Name, inputTodoItemDTO.IsComplete);
        }

        public void Delete(int id)
        {
            todoItemService.Delete(id);
        }
    }
}