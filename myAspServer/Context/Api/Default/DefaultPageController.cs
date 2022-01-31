namespace myAspServer.Context.Api.Default
{
    public static class DefaultPageController
    {
        public static void Init(WebApplication app)
        {
            app.MapGet("/", () => "Hello world");
        }
    }
}
