using myAspServer.Context.Api;
using myAspServer.Context.Database;

var builder = WebApplication.CreateBuilder(args);

DatabaseBuilder.Build(builder);

var app = builder.Build();

ApiBuilder.BuildDefaultPageApi().Init(app);
ApiBuilder.BuildTodoApi().Init(app);

app.Run();
