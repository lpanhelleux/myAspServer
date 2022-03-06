namespace myAspServer.Model.Common.Entity
{
    public interface ITodoResult
    {
        object? Value { get; }

        ITodoResultsEnum Code { get; }
    }
}
