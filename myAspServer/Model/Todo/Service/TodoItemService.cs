namespace myAspServer.Model.Todo.Service
{
    using myAspServer.Model.Todo.Entity;
    using myAspServer.Model.Todo.Repository;

    public class TodoItemService : ITodoItemService
    {
        public ITodoItemRepository? TodoItemRepository { get; set; }

        public void Create(TodoItemEntity todoItem) => TodoItemRepository?.Add(todoItem);

        public IResult Delete(int id)
        {
            return TodoItemRepository != null ? TodoItemRepository.Delete(id).Result : Results.NotFound();
        }

        public TodoItemEntity? Get(int id)
        {
            return TodoItemRepository != null ? TodoItemRepository.Get(id).Result : null;
        }

        public IList<TodoItemEntity> GetAll()
        {
            return TodoItemRepository != null ? TodoItemRepository.GetAll().Result : new List<TodoItemEntity>();
        }

        public IResult Update(int id, string? name, bool isComplete)
        {
            return TodoItemRepository != null ? TodoItemRepository.Update(id, name, isComplete).Result : Results.StatusCode(500);
        }
    }
}
