namespace myAspServer.Model.Common.Entity
{
    public class TodoResults
    {
        public static ITodoResult Ok(object? value)
        {
            return new TodoResult(ITodoResultsEnum.OK, value);
        }

        public static ITodoResult NotFound()
        {
            return new TodoResult(ITodoResultsEnum.NotFound);
        }

        internal static ITodoResult NoContent()
        {
            return new TodoResult(ITodoResultsEnum.NoContent);
        }
    }
}
