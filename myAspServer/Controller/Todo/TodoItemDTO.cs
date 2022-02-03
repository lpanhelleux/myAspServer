namespace myAspServer.Controller.Todo
{
    using Model.Todo.Entity;

    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }

        public TodoItemDTO()
        { 
        }

        public TodoItemDTO(TodoItemEntity todoItem)
        {
            (Id, Name, IsComplete) = (todoItem.Id, todoItem.Name, todoItem.IsComplete);
        }
    }
}
    