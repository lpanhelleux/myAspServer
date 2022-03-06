namespace myAspServer.Context.Api
{
    using myAspServer.Context.Database;
    using myAspServer.Controller.ControllerResults;
    using myAspServer.Controller.User;
    using myAspServer.Model.User.Repository;
    using myAspServer.Model.User.Service;
    using System.Text.Json;

    public class UserApi
    {
        public void Init(WebApplication app)
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

            app.MapGet("/users", (TodoDbContext dbContext) =>
            {
                var controller = BuildController(dbContext);
                return controller.GetAll();
            });

            app.MapGet("/users/{id}", (int id, TodoDbContext dbContext) =>
            {
                var controller = BuildController(dbContext);
                return controller.Get(id);
            });

            app.MapPost("/users", (UserDTO userDTO, TodoDbContext dbContext) =>
            {
                var controller = BuildController(dbContext);
                IControllerResult result = controller.Post(userDTO);

                return Results.Created($"/users/{userDTO.Id}", result.Value);
            });

            app.MapPut("/users/{id}", (int id, UserDTO userDTO, TodoDbContext dbContext) =>
            {
                var controller = BuildController(dbContext);
                controller.Put(id, userDTO);
            });

            app.MapDelete("/users/{id}", (int id, TodoDbContext dbContext) =>
            {
                var controller = BuildController(dbContext);
                controller.Delete(id);
            });
        }

        private static UserController BuildController(TodoDbContext dbContext)
        {
            var repository = UserRepositoryBuilder.Build(dbContext);
            var service = UserServiceBuilder.Build(repository);
            var controller = UserControllerBuilder.Build(service);
            return controller;
        }
    }
}
