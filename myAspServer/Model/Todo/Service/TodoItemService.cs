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

        public TodoItemEntity? Get(int id)
        {
            if (todoItemRepository != null)
            {
                return todoItemRepository.Get(id).Result;
            }

            return null;
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
