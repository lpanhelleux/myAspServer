namespace myAspServer.Model.Todo.Service
{
    using myAspServer.Model.Todo.Entity;
    using myAspServer.Model.Todo.Repository;

    public class TodoItemService : ITodoItemService
    {
        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            TodoItemRepository = todoItemRepository;
        }

        private ITodoItemRepository TodoItemRepository { get; }

        public void Create(TodoItemEntity todoItem) => TodoItemRepository.Add(todoItem);

        public ITodoItemResult Delete(int id)
        {
            return TodoItemRepository.Delete(id).Result;
        }

        public TodoItemEntity? Get(int id)
        {
            return TodoItemRepository.Get(id).Result;
        }

        public IList<TodoItemEntity> GetAll()
        {
            return TodoItemRepository.GetAll().Result;
        }

        public ITodoItemResult Update(int id, string? name, bool isComplete)
        {
            return TodoItemRepository.Update(id, name, isComplete).Result;
        }
    }
}
