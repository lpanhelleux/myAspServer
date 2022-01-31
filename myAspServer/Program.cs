using myAspServer.Context.Database;
using myAspServer.Context.Api.Todo;
using myAspServer.Context.Api.Default;

var builder = WebApplication.CreateBuilder(args);

DatabaseBuilder.Build(builder);

var app = builder.Build();

DefaultPageController.Init(app);
TodoController.Init(app);

app.Run();
