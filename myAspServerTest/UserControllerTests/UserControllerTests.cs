namespace myAspServerTest.UserControllerTests
{

    using Microsoft.EntityFrameworkCore;
    using myAspServer.Context.Database;
    using myAspServer.Controller.ControllerResults;
    using myAspServer.Controller.User;
    using myAspServer.Model.User.Repository;
    using myAspServer.Model.User.Service;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class UserControllerTests
    {
        private readonly IUserController userController;

        public UserControllerTests()
        {
            TodoDbContext todoDbContext = BuildDbContext();
            userController = BuildController(todoDbContext);
        }

        [Fact]
        public void Post()
        {
            UserDTO john = new()
            {
                Name = "John",
            };

            IControllerResult result = userController.Post(john);

            UserDTO? expected = result.Value as UserDTO;
            Assert.NotNull(expected);
            Assert.True(expected?.Id > 0);
        }

        [Fact]
        public void Get()
        {
            UserDTO john = new()
            {
                Name = "John",
            };

            IControllerResult resultPost = userController.Post(john);

            if (resultPost.Value is UserDTO expected)
            {
                IControllerResult resultGet = userController.Get(expected.Id);
                UserDTO? actual = resultGet.Value as UserDTO;
                Equal(actual, expected);
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void GetAll()
        {
            // Get all todos before add todos
            IControllerResult resultGetAllBefore = userController.GetAll();
            IList<UserDTO>? todosBefore = resultGetAllBefore.Value as IList<UserDTO>;

            // Add one todo
            UserDTO john = new()
            {
                Name = "John",
            };

            UserDTO? expectedJohn = userController.Post(john).Value as UserDTO;

            // Add second todo
            UserDTO tess = new()
            {
                Name = "Tess",
            };

            UserDTO? expectedTess = userController.Post(tess).Value as UserDTO;

            // Get all todos after add todos
            IControllerResult resultGetAllAfter = userController.GetAll();
            IList<UserDTO>? todosAfter = resultGetAllAfter.Value as IList<UserDTO>;

            Assert.Equal(2, todosAfter?.Count - todosBefore?.Count);
            UserDTO? actualJohn = todosAfter?.SingleOrDefault(t => t.Id == expectedJohn?.Id);
            UserDTO? actualTess = todosAfter?.SingleOrDefault(t => t.Id == expectedTess?.Id);

            Equal(actualJohn, expectedJohn);
            Equal(actualTess, expectedTess);
        }

        [Fact]
        public void DeleteOK()
        {
            UserDTO john = new()
            {
                Name = "John",
            };

            IControllerResult resultPost = userController.Post(john);
            UserDTO expected = GetUserFromPostResult(resultPost);

            userController.Delete(expected.Id);

            IControllerResult resultGet = userController.Get(expected.Id);
            Assert.Equal(ControllerResultsEnum.NotFound, resultGet.Result);
        }

        [Fact]
        public void DeleteNotFound()
        {
            var invalidId = 100;
            IControllerResult resultGet = userController.Delete(invalidId);
            Assert.Equal(ControllerResultsEnum.NotFound, resultGet.Result);
        }

        private static UserDTO GetUserFromPostResult(IControllerResult resultPost)
        {
            if (resultPost.Value is not UserDTO user)
            {
                Assert.True(false);
                return new UserDTO();
            }

            return user;
        }

        [Fact]
        public void PutOK()
        {
            UserDTO john = new()
            {
                Name = "John",
            };

            IControllerResult resultPost = userController.Post(john);
            UserDTO johnV1 = GetUserFromPostResult(resultPost);

            UserDTO johnV2 = new()
            {
                Name = "John V2",
            };

            var resultPut = userController.Put(johnV1.Id, johnV2);
            Assert.Equal(ControllerResultsEnum.NotContent, resultPut.Result);

            IControllerResult resultGet = userController.Get(johnV1.Id);
            UserDTO? actual = resultGet.Value as UserDTO;

            Assert.Equal(ControllerResultsEnum.OK, resultGet.Result);
            Assert.Equal(actual?.Name, johnV2.Name);
        }

        [Fact]
        public void PutNotFound()
        {
            UserDTO user = new()
            {
                Name = "John V2",
            };

            var invalidId = 100;

            var resultPut = userController.Put(invalidId, user);
            Assert.Equal(ControllerResultsEnum.NotFound, resultPut.Result);
        }

        private static TodoDbContext BuildDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TodoDbContext>();
            optionsBuilder.UseInMemoryDatabase("TodoApplication");
            TodoDbContext todoDbContext = new(optionsBuilder.Options);
            return todoDbContext;
        }

        private static IUserController BuildController(TodoDbContext todoDbContext)
        {
            IUserRepository userRepository = UserRepositoryBuilder.Build(todoDbContext);
            IUserService userService = UserServiceBuilder.Build(userRepository);
            return UserControllerBuilder.Build(userService);
        }

        private static void Equal(UserDTO actual, UserDTO expected)
        {
            Assert.Equal(actual.Id, expected.Id);
            Assert.Equal(actual.Name, expected.Name);
        }
    }
}
