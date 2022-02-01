namespace myAspServer.Model.Todo.Repository
{
    using myAspServer.Model.Todo.Entity;

    public interface ITodoItemRepository
    {

        void Add(TodoItemEntity todoItem);

        Task<IResult> Delete(int id);

        Task<IResult> Update(int id, string? name, bool isComplete);

        Task<TodoItemEntity?> Get(int id);

        Task<IList<TodoItemEntity>> GetAll();
    }
}
