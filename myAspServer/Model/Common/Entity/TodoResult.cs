namespace myAspServer.Model.Common.Entity
{
    public class TodoResult : ITodoResult
    {
        public object? Value { get; protected set; }

        public ITodoResultsEnum Code { get; protected set; }

        public TodoResult(ITodoResultsEnum code, object? value)
        {
            Code = code;
            Value = value;
        }

        public TodoResult(ITodoResultsEnum result)
        {
            Code = result;
        }
    }
}
