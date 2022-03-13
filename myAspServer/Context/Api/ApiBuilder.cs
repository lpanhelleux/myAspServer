namespace myAspServer.Context.Api
{
    public static class ApiBuilder
    {
        public static TodoApi BuildTodoApi()
        {
            return new TodoApi();
        }

        public static UserApi BuildUserApi()
        {
            return new UserApi();
        }

        public static DefaultPageApi BuildDefaultPageApi()
        {
            return new DefaultPageApi();
        }
    }
}
