using myAspServer.Context.Api;
using myAspServer.Context.Database;

var builder = WebApplication.CreateBuilder(args);

var policy =Cors.Build(builder);
DatabaseBuilder.Build(builder);

var app = builder.Build();

ApiBuilder.BuildDefaultPageApi().Init(app);
ApiBuilder.BuildTodoApi().Init(app);
ApiBuilder.BuildUserApi().Init(app);

app.UseCors(policy);
app.Run();
