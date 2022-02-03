namespace myAspServer.Context.Api
{
    public static class ApiBuilder
    {        
        public static TodoApi BuildTodoApi()
        {
            return new TodoApi();
        }

        public static DefaultPageApi BuildDefaultPageApi()
        {
            return new DefaultPageApi();
        }
    }
}
