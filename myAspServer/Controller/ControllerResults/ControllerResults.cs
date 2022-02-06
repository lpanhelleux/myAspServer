namespace myAspServer.Controller.ControllerResults
{
    public class ControllerResults
    {
        public static IControllerResult Ok(object? value)
        {
            return new ControllerResult(ControllerResultsEnum.OK, value);
        }

        public static IControllerResult NotFound()
        {
            return new ControllerResult(ControllerResultsEnum.NotFound);
        }

        internal static IControllerResult NoContent()
        {
            return new ControllerResult(ControllerResultsEnum.NotContent);
        }
    }
}
