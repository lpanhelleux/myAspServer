namespace myAspServer.Model.Todo.Service
{
    using myAspServer.Model.Todo.Entity;

    public interface ITodoItemService
    {
        void Add(TodoItemEntity todoItem);

        IResult Delete(int id);

        void Update(TodoItemEntity todoItem);

        void Get(TodoItemEntity todoItem);

        IList<TodoItemEntity> GetAll();
    }
}
