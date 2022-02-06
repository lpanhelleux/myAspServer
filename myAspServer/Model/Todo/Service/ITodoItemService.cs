namespace myAspServer.Model.Todo.Service
{
    using myAspServer.Model.Todo.Entity;

    public interface ITodoItemService
    {
        void Create(TodoItemEntity todoItem);

        ITodoItemResult Delete(int id);

        ITodoItemResult Update(int id, string? name, bool isComplete);

        TodoItemEntity? Get(int id);

        IList<TodoItemEntity> GetAll();
    }
}
