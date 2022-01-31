namespace myAspServer.Context.Database
{
    using Microsoft.EntityFrameworkCore;

    public static class DatabaseBuilder
    {
        public static void Build(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));
        }
    }
}
