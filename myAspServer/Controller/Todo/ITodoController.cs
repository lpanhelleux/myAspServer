namespace myAspServer.Controller.Todo
{
    public interface ITodoController
    {
        void Delete(int id);
        IResult Get(int id);
        IResult GetAll();
        IResult Post(TodoItemDTO todoItemDTO);
        IResult Put(int id, TodoItemDTO inputTodoItemDTO);
    }
}