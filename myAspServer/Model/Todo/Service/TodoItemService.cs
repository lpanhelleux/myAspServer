namespace myAspServer.Model.Todo.Service
{
    using myAspServer.Model.Todo.Entity;
    using myAspServer.Model.Todo.Repository;

    public class TodoItemService : ITodoItemService
    {
        public ITodoItemRepository? TodoItemRepository { get; set; }

        public void Create(TodoItemEntity todoItem)
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

        public IResult Update(int id, string? name, bool isComplete)
        {
            if (TodoItemRepository != null)
            {
                return TodoItemRepository.Update(id, name, isComplete).Result;
            }

            return Results.StatusCode(500);
        }
    }
}
