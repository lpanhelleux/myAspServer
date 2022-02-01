namespace myAspServer.Model.Todo.Service
{
    using myAspServer.Model.Todo.Entity;

    public interface ITodoItemService
    {
        void Add(TodoItemEntity todoItem);

        IResult Delete(int id);

        void Update(TodoItemEntity todoItem);

        TodoItemEntity? Get(int id);

        IList<TodoItemEntity> GetAll();
    }
}
