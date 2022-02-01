namespace myAspServer.Model.Todo.Service
{
    using myAspServer.Model.Todo.Entity;
    using myAspServer.Model.Todo.Repository;

    public class TodoItemService : ITodoItemService
    {
        public ITodoItemRepository? TodoItemRepository { get; set; }

        public void Add(TodoItemEntity todoItem)
        {
            TodoItemRepository?.Add(todoItem);
        }

        public IResult Delete(int id)
        {
            if (TodoItemRepository != null)
            {
                return TodoItemRepository.Delete(id).Result;
            }

            return Results.NotFound();
        }

        public TodoItemEntity? Get(int id)
        {
            if (TodoItemRepository != null)
            {
                return TodoItemRepository.Get(id).Result;
            }

            return null;
        }

        public IList<TodoItemEntity> GetAll()
        {
            if (TodoItemRepository != null)
            {
                return TodoItemRepository.GetAll().Result;
            }

            return new List<TodoItemEntity>();
        }

        public void Update(TodoItemEntity todoItem)
        {
            throw new NotImplementedException();
        }
    }
}
