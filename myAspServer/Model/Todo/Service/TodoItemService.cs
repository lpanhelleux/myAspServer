namespace myAspServer.Model.Todo.Service
{
    using myAspServer.Model.Todo.Entity;
    using myAspServer.Model.Todo.Repository;

    public class TodoItemService : ITodoItemService
    {
        public ITodoItemRepository? todoItemRepository { get; set; }

        public void Add(TodoItemEntity todoItem)
        {
            todoItemRepository?.Add(todoItem);
        }

        public IResult Delete(int id)
        {
            if (todoItemRepository != null)
            {
                return todoItemRepository.Delete(id).Result;
            }

            return Results.NotFound();
        }

        public void Get(TodoItemEntity todoItem)
        {
            throw new NotImplementedException();
        }

        public IList<TodoItemEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(TodoItemEntity todoItem)
        {
            throw new NotImplementedException();
        }
    }
}
