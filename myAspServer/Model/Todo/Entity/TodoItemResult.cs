namespace myAspServer.Model.Todo.Entity
{
    public class TodoItemResult : ITodoItemResult
    {
        public object? Value { get; protected set; }

        public ITodoItemResultsEnum Code { get; protected set; }

        public TodoItemResult(ITodoItemResultsEnum code, object? value)
        {
            Code = code;
            Value = value;
        }

        public TodoItemResult(ITodoItemResultsEnum result)
        {
            Code = result;
        }
    }
}
