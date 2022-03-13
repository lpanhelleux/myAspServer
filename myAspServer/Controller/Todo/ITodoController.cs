namespace myAspServer.Controller.Todo
{
    using myAspServer.Controller.ControllerResults;

    public interface ITodoController
    {
        IControllerResult Delete(int id);
        IControllerResult Get(int id);
        IControllerResult GetAll();
        IControllerResult Post(TodoItemDTO todoItemDTO);
        IControllerResult Put(int id, TodoItemDTO todoItemDTO);
        IControllerResult GetAllTodosByUserId(int userId);
    }
}