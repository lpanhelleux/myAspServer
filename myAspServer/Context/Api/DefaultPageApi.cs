namespace myAspServer.Context.Api
{
    public class DefaultPageApi
    {
        public void Init(WebApplication app)
        {
            app.MapGet("/", () => "Hello world");
        }
    }
}
