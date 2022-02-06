namespace myAspServer.Controller.ControllerResults
{
    public class ControllerResult : IControllerResult
    {
        public object? Value { get; protected set; }

        public ControllerResultsEnum Result { get; protected set; }

        public ControllerResult(ControllerResultsEnum result, object? value)
        {
            Result = result;
            Value = value;
        }

        public ControllerResult(ControllerResultsEnum result)
        {
            Result = result;
        }
    }
}
