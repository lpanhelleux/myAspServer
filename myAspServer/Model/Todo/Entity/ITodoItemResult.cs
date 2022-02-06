namespace myAspServer.Model.Todo.Entity
{
    public interface ITodoItemResult
    {
        object? Value { get; }

        ITodoItemResultsEnum Code { get; }
    }
}
