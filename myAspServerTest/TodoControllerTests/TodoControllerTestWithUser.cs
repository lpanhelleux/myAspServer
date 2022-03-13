namespace myAspServerTest.TodoControllerTests
{
    using Microsoft.EntityFrameworkCore;
    using myAspServer.Context.Database;
    using myAspServer.Controller.ControllerResults;
    using myAspServer.Controller.Todo;
    using myAspServer.Controller.User;
    using myAspServer.Model.Todo.Repository;
    using myAspServer.Model.Todo.Service;
    using myAspServer.Model.User.Repository;
    using myAspServer.Model.User.Service;
    using System.Collections.Generic;
    using Xunit;

    public class TodoControllerTestWithUser
    {
        private readonly ITodoController todoController;
        private readonly IUserController userController;

        public TodoControllerTestWithUser()
        {
            TodoDbContext todoDbContext = BuildDbContext();
            todoController = BuildTodoController(todoDbContext);
            userController = BuildUserController(todoDbContext);
        }

        [Fact]
        public void PostWithUser()
        {
            // Add user
            UserDTO john = new()
            {
                Name = "John",
            };

            IControllerResult userResult = userController.Post(john);

            UserDTO? userExpected = userResult.Value as UserDTO;
            Assert.NotNull(userExpected);
            Assert.True(userExpected?.Id > 0);

            // Add toto
            TodoItemDTO bigDog = new()
            {
                Name = "Big dog",
                IsComplete = true,
                UserId = userExpected.Id,
            };

            IControllerResult result = todoController.Post(bigDog);
            Assert.Equal(ControllerResultsEnum.OK, result.Result);

            TodoItemDTO? todoObtained = result.Value as TodoItemDTO;
            Assert.NotNull(todoObtained);
            Assert.True(todoObtained?.Id > 0);
            Assert.Equal(bigDog.UserId, todoObtained?.UserId);
        }

        [Fact]
        public void PostManyTodoForUser()
        {
            // Add user
            UserDTO? john = new()
            {
                Name = "John",
            };

            IControllerResult userResult = userController.Post(john);

            john = userResult.Value as UserDTO;

            // Check before add
            IControllerResult result = todoController.GetAllTodosByUserId(john.Id);
            IList<TodoItemDTO>? todosBefore = result.Value as IList<TodoItemDTO>;

            // Add todos for John
            for (int i = 0; i < 5; i++)
            {
                TodoItemDTO bigDog = new()
                {
                    Name = "Big dog " + i,
                    IsComplete = true,
                    UserId = john.Id,
                };

                todoController.Post(bigDog);
            }

            // Get all todos by user
            result = todoController.GetAllTodosByUserId(john.Id);
            IList<TodoItemDTO>? todosAfter = result.Value as IList<TodoItemDTO>;
            Assert.Equal(todosBefore?.Count + 5, todosAfter?.Count);
        }

        private static TodoDbContext BuildDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TodoDbContext>();
            optionsBuilder.UseInMemoryDatabase("TodoList");
            TodoDbContext todoDbContext = new(optionsBuilder.Options);
            return todoDbContext;
        }

        private static ITodoController BuildTodoController(TodoDbContext todoDbContext)
        {
            ITodoItemRepository todoItemRepository = TodoItemRepositoryBuilder.Build(todoDbContext);
            ITodoItemService todoItemService = TodoItemServiceBuilder.Build(todoItemRepository);
            return TodoControllerBuilder.Build(todoItemService);
        }

        private static IUserController BuildUserController(TodoDbContext todoDbContext)
        {
            IUserRepository userRepository = UserRepositoryBuilder.Build(todoDbContext);
            IUserService userService = UserServiceBuilder.Build(userRepository);
            return UserControllerBuilder.Build(userService);
        }
    }
}
