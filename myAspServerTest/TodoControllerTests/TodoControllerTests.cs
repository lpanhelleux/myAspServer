namespace myAspServerTest.TodoControllerTests
{
    using Xunit;

    using Microsoft.EntityFrameworkCore;
    using myAspServer.Context.Database;
    using myAspServer.Controller.ControllerResults;
    using myAspServer.Controller.Todo;
    using myAspServer.Model.Todo.Repository;
    using myAspServer.Model.Todo.Service;
    using System.Collections.Generic;
    using System.Linq;
    
    public class TodoControllerTests
    {
        private readonly ITodoController todoController;

        public TodoControllerTests()
        {
           TodoDbContext todoDbContext = BuildDbContext();
            todoController = BuildController(todoDbContext);
        }

        [Fact]
        public void Post()
        {
            TodoItemDTO bigDog = new()
            {
                Name = "Big dog",
                IsComplete = true,
            };

            IControllerResult result = todoController.Post(bigDog);

            TodoItemDTO? expected = result.Value as TodoItemDTO;
            Assert.NotNull(expected);
            Assert.True(expected?.Id > 0);
        }

        [Fact]
        public void Get()
        {
            TodoItemDTO bigDog = new()
            {
                Name = "Big dog",
                IsComplete = true
            };

            IControllerResult resultPost = todoController.Post(bigDog);

            if (resultPost.Value is TodoItemDTO expected)
            {
                IControllerResult resultGet = todoController.Get(expected.Id);
                TodoItemDTO? actual = resultGet.Value as TodoItemDTO;
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
            IControllerResult resultGetAllBefore = todoController.GetAll();
            IList<TodoItemDTO>? todosBefore = resultGetAllBefore.Value as IList<TodoItemDTO>;

            // Add one todo
            TodoItemDTO bigDog = new()
            {
                Name = "Big dog",
                IsComplete = true
            };

            TodoItemDTO? expectedBigDog = todoController.Post(bigDog).Value as TodoItemDTO;

            // Add second todo
            TodoItemDTO redFish = new()
            {
                Name = "Red fish",
                IsComplete = true
            };

            TodoItemDTO? expectedRedFish = todoController.Post(redFish).Value as TodoItemDTO;

            // Get all todos after add todos
            IControllerResult resultGetAllAfter = todoController.GetAll();
            IList<TodoItemDTO>? todosAfter = resultGetAllAfter.Value as IList<TodoItemDTO>;

            Assert.Equal(2, todosAfter?.Count - todosBefore?.Count);
            TodoItemDTO? actualBigDog = todosAfter?.SingleOrDefault(t => t.Id == expectedBigDog?.Id);
            TodoItemDTO? actualRedFIsh = todosAfter?.SingleOrDefault(t => t.Id == expectedRedFish?.Id);

            Equal(actualBigDog, expectedBigDog);
            Equal(actualRedFIsh, expectedRedFish);
        }

        [Fact]
        public void DeleteOK()
        {
            TodoItemDTO bigDog = new()
            {
                Name = "Big dog",
                IsComplete = true
            };

            IControllerResult resultPost = todoController.Post(bigDog);
            TodoItemDTO expected = GetTodoItemFromPostResult(resultPost);

            todoController.Delete(expected.Id);

            IControllerResult resultGet = todoController.Get(expected.Id);
            Assert.Equal(ControllerResultsEnum.NotFound, resultGet.Result);
        }

        [Fact]
        public void DeleteNotFound()
        {
            var invalidId = 100;
            IControllerResult resultGet = todoController.Delete(invalidId);
            Assert.Equal(ControllerResultsEnum.NotFound, resultGet.Result);
        }
        
        [Fact]
        public void PutOK()
        {
            TodoItemDTO bigDog = new()
            {
                Name = "Big dog",
                IsComplete = true
            };

            IControllerResult resultPost = todoController.Post(bigDog);
            TodoItemDTO bigDogV1 = GetTodoItemFromPostResult(resultPost);

            TodoItemDTO bigDogV2 = new()
            {
                Name = "Big dog V2",
                IsComplete = false,
            };

            var resultPut = todoController.Put(bigDogV1.Id, bigDogV2);
            Assert.Equal(ControllerResultsEnum.NotContent, resultPut.Result);

            IControllerResult resultGet = todoController.Get(bigDogV1.Id);
            TodoItemDTO? actual = resultGet.Value as TodoItemDTO;

            Assert.Equal(ControllerResultsEnum.OK, resultGet.Result);
            Assert.Equal(actual?.Name, bigDogV2.Name);
            Assert.Equal(actual?.IsComplete, bigDogV2.IsComplete);
        }

        [Fact]
        public void PutNotFound()
        {
            TodoItemDTO bigDog = new()
            {
                Name = "Big dog V2",
                IsComplete = false,
            };

            var invalidId = 100;

            var resultPut = todoController.Put(invalidId, bigDog);
            Assert.Equal(ControllerResultsEnum.NotFound, resultPut.Result);
        }

        private static TodoItemDTO GetTodoItemFromPostResult(IControllerResult resultPost)
        {
            if (resultPost.Value is not TodoItemDTO todoItem)
            {
                Assert.True(false);
                return new TodoItemDTO();
            }

            return todoItem;
        }

        private static TodoDbContext BuildDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TodoDbContext>();
            optionsBuilder.UseInMemoryDatabase("TodoList");
            TodoDbContext todoDbContext = new(optionsBuilder.Options);
            return todoDbContext;
        }

        private static ITodoController BuildController(TodoDbContext todoDbContext)
        {
            ITodoItemRepository todoItemRepository = TodoItemRepositoryBuilder.Build(todoDbContext);
            ITodoItemService todoItemService = TodoItemServiceBuilder.Build(todoItemRepository);
            return TodoControllerBuilder.Build(todoItemService);
        }

        private static void Equal(TodoItemDTO actual, TodoItemDTO expected)
        {
            Assert.Equal(actual.Id, expected.Id);
            Assert.Equal(actual.IsComplete, expected.IsComplete);
            Assert.Equal(actual.Name, expected.Name);
        }
    }
}