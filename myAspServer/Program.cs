using myAspServer.Context.Api.Default;
using myAspServer.Context.Api.Todo;
using myAspServer.Context.Database;

var builder = WebApplication.CreateBuilder(args);

DatabaseBuilder.Build(builder);

var app = builder.Build();

DefaultPageController.Init(app);

TodoApi todoApi = new TodoApi();
todoApi.Init(app);

app.Run();
