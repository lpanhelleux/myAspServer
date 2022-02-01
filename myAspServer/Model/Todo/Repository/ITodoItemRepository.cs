namespace myAspServer.Model.Todo.Repository
{
    using myAspServer.Model.Todo.Entity;

    public interface ITodoItemRepository
    {

        void Add(TodoItemEntity todoItem);

        Task<IResult> Delete(int id);

        void Update(TodoItemEntity todoItem);

        Task<TodoItemEntity?> Get(int id);

        Task<IList<TodoItemEntity>> GetAll();
    }
}
