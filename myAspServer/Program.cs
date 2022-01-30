using myAspServer.Context.Database;
using myAspServer.Context.Api.Todo;

var builder = WebApplication.CreateBuilder(args);

DatabaseBuilder.Build(builder);

var app = builder.Build();

app.MapGet("/", () => "Hello world");

TodoControllerBuilder.Init(app);

app.Run();
