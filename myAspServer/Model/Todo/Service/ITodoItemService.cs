namespace myAspServer.Model.Todo.Service
{
    using myAspServer.Model.Todo.Entity;

    public interface ITodoItemService
    {
        void Create(TodoItemEntity todoItem);

        IResult Delete(int id);

        IResult Update(int id, string? name, bool isComplete);

        TodoItemEntity? Get(int id);

        IList<TodoItemEntity> GetAll();
    }
}
