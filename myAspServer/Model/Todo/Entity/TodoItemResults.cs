namespace myAspServer.Model.Todo.Entity
{
    public class TodoItemResults
    {
        public static ITodoItemResult Ok(object? value)
        {
            return new TodoItemResult(ITodoItemResultsEnum.OK, value);
        }

        public static ITodoItemResult NotFound()
        {
            return new TodoItemResult(ITodoItemResultsEnum.NotFound);
        }

        internal static ITodoItemResult NoContent()
        {
            return new TodoItemResult(ITodoItemResultsEnum.NoContent);
        }
    }
}
