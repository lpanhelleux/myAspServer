namespace myAspServer.Controller.Todo
{
    using myAspServer.Controller.ControllerResults;
    using myAspServer.Model.Todo.Entity;
    using myAspServer.Model.Todo.Service;

    public class TodoController : ITodoController
    {
        private readonly ITodoItemService todoItemService;

        public TodoController(ITodoItemService todoItemService)
        {
            this.todoItemService = todoItemService;
        }

        public IControllerResult GetAll()
        {
            IList<TodoItemDTO> todoItemDTOs = todoItemService.GetAll().Select(x => new TodoItemDTO(x)).ToList();
            return ControllerResults.Ok(todoItemDTOs);
        }

        public IControllerResult Get(int id)
        {
            return todoItemService.Get(id) is TodoItemEntity todoItemEntity
                ? ControllerResults.Ok(new TodoItemDTO(todoItemEntity))
                : ControllerResults.NotFound();
        }

        public IControllerResult Post(TodoItemDTO todoItemDTO)
        {
            TodoItemEntity todo = new()
            {
                Id = todoItemDTO.Id,
                Name = todoItemDTO.Name,
                IsComplete = todoItemDTO.IsComplete,
            };

            todoItemService.Create(todo);
            return ControllerResults.Ok(new TodoItemDTO(todo));
        }

        public IControllerResult Put(int id, TodoItemDTO inputTodoItemDTO)
        {
            ITodoItemResult todoItemResult = todoItemService.Update(id, inputTodoItemDTO.Name, inputTodoItemDTO.IsComplete);

            return todoItemResult.Code == ITodoItemResultsEnum.NoContent
                ? ControllerResults.NoContent()
                : ControllerResults.NotFound();
        }

        public IControllerResult Delete(int id)
        {
            ITodoItemResult todoItemResult = todoItemService.Delete(id);

            return todoItemResult.Code == ITodoItemResultsEnum.NoContent
                ? ControllerResults.NoContent()
                : ControllerResults.NotFound();
        }
    }
}