namespace myAspServer.Model.Todo.Repository
{
    using myAspServer.Model.Common.Entity;
    using myAspServer.Model.Todo.Entity;

    public interface ITodoItemRepository
    {

        void Add(TodoItemEntity todoItem);

        Task<ITodoResult> Delete(int id);

        Task<ITodoResult> Update(int id, string? name, bool isComplete);

        Task<TodoItemEntity?> Get(int id);

        Task<IList<TodoItemEntity>> GetAll();
    }
}
