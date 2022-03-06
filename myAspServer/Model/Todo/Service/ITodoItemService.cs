namespace myAspServer.Model.Todo.Service
{
    using myAspServer.Model.Common.Entity;
    using myAspServer.Model.Todo.Entity;

    public interface ITodoItemService
    {
        void Create(TodoItemEntity todoItem);
        ITodoResult Delete(int id);
        ITodoResult Update(int id, string? name, bool isComplete);
        TodoItemEntity? Get(int id);
        IList<TodoItemEntity> GetAll();
    }
}
