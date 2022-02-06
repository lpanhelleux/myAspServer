namespace myAspServer.Controller.ControllerResults
{
    public interface IControllerResult
    {
        object? Value { get; }

        ControllerResultsEnum Result { get; }
    }
}
