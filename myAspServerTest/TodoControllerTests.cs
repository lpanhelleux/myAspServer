namespace myAspServerTest
{
    using Microsoft.EntityFrameworkCore;
    using myAspServer.Context.Database;
    using myAspServer.Controller.ControllerResults;
    using myAspServer.Controller.Todo;
    using myAspServer.Model.Todo.Repository;
    using myAspServer.Model.Todo.Service;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using Is = NUnit.DeepObjectCompare.Is;
    
    public class TodoControllerTests
    {
        private ITodoController todoController;
        
        [SetUp]
        public void Setup()
        {
            TodoDbContext todoDbContext = BuildDbContext();
            todoController = BuildController(todoDbContext);
        }

        [Test]
        public void Post()
        {
            TodoItemDTO bigDog = new()
            {
                Name = "Big dog",
                IsComplete = true
            };

            IControllerResult result = todoController.Post(bigDog);

            TodoItemDTO? expected = result.Value as TodoItemDTO;
            Assert.IsNotNull(expected);
            Assert.Greater(expected?.Id, 0);
        }

        [Test]
        public void Get()
        {
            TodoItemDTO bigDog = new()
            {
                Name = "Big dog",
                IsComplete = true
            };

            IControllerResult resultPost = todoController.Post(bigDog);
            TodoItemDTO? expected = resultPost.Value as TodoItemDTO;

            IControllerResult resultGet = todoController.Get(expected.Id);
            TodoItemDTO? actual = resultGet.Value as TodoItemDTO;

            Assert.That(actual, Is.DeepEqualTo(expected));
        }

        [Test]
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

            Assert.AreEqual(2, todosAfter?.Count - todosBefore?.Count);
            TodoItemDTO? actualBigDog = todosAfter?.SingleOrDefault(t => t.Id == expectedBigDog?.Id);
            TodoItemDTO? actualRedFIsh = todosAfter?.SingleOrDefault(t => t.Id == expectedRedFish?.Id);

            Assert.That(actualBigDog, Is.DeepEqualTo(expectedBigDog));
            Assert.That(actualRedFIsh, Is.DeepEqualTo(expectedRedFish));
        }

        [Test]
        public void Delete()
        {
            TodoItemDTO bigDog = new()
            {
                Name = "Big dog",
                IsComplete = true
            };

            IControllerResult resultPost = todoController.Post(bigDog);
            TodoItemDTO? expected = resultPost.Value as TodoItemDTO;

            todoController.Delete(expected.Id);

            IControllerResult resultGet = todoController.Get(expected.Id);
            Assert.AreEqual(ControllerResultsEnum.NotFound, resultGet.Result);
        }

        [Test]
        public void Put()
        {
            TodoItemDTO bigDog = new()
            {
                Name = "Big dog",
                IsComplete = true
            };

            IControllerResult resultPost = todoController.Post(bigDog);
            TodoItemDTO? bigDogV1 = resultPost.Value as TodoItemDTO;

            TodoItemDTO bigDogV2 = new()
            {
                Name = "Big dog V2",
                IsComplete = false,
            };

            IControllerResult? resultPut = todoController.Put(bigDogV1.Id, bigDogV2);
            Assert.AreEqual(ControllerResultsEnum.NotContent, resultPut.Result);

            IControllerResult resultGet = todoController.Get(bigDogV1.Id);
            TodoItemDTO? actual = resultGet.Value as TodoItemDTO;
            
            Assert.AreEqual(ControllerResultsEnum.OK, resultGet.Result);
            Assert.AreEqual(actual.Name, bigDogV2.Name);
            Assert.AreEqual(actual.IsComplete, bigDogV2.IsComplete);
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
    }
}