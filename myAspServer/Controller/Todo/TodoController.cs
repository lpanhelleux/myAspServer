namespace myAspServer.Controller.Todo
{
    using myAspServer.Controller.ControllerResults;
    using myAspServer.Model.Common.Entity;
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
            IList<TodoItemDTO> todoItemDTOs = todoItemService.GetAll().Select(x => Build(x)).ToList();
            return ControllerResults.Ok(todoItemDTOs);
        }

        public IControllerResult Get(int id)
        {
            return todoItemService.Get(id) is TodoItemEntity todoItemEntity
                ? ControllerResults.Ok(Build(todoItemEntity))
                : ControllerResults.NotFound();
        }

        public IControllerResult Post(TodoItemDTO todoItemDTO)
        {
            TodoItemEntity todo = new()
            {
                Id = todoItemDTO.Id,
                Name = todoItemDTO.Name,
                IsComplete = todoItemDTO.IsComplete,
                UserId = todoItemDTO.UserId
            };

            todoItemService.Create(todo);
            return ControllerResults.Ok(Build(todo));
        }

        public IControllerResult Put(int id, TodoItemDTO todoItemDTO)
        {
            ITodoResult todoItemResult = todoItemService.Update(id, todoItemDTO.Name, todoItemDTO.IsComplete);

            return todoItemResult.Code == ITodoResultsEnum.NoContent
                ? ControllerResults.NoContent()
                : ControllerResults.NotFound();
        }

        public IControllerResult Delete(int id)
        {
            ITodoResult todoItemResult = todoItemService.Delete(id);

            return todoItemResult.Code == ITodoResultsEnum.NoContent
                ? ControllerResults.NoContent()
                : ControllerResults.NotFound();
        }

        public IControllerResult GetAllTodosByUserId(int userId)
        {
            IList<TodoItemDTO> todoItemDTOs = todoItemService.GetAllTodosByUserId(userId).Select(x => Build(x)).ToList();
            return ControllerResults.Ok(todoItemDTOs);

        }

        public static TodoItemDTO Build(TodoItemEntity todoItem)
        {
            return new TodoItemDTO()
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete,
                UserId = todoItem.UserId
            };
        }
    }
}