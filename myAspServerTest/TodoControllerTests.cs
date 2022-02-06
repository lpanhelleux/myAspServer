namespace myAspServerTest
{
    using Microsoft.EntityFrameworkCore;
    using myAspServer.Context.Database;
    using myAspServer.Controller.ControllerResults;
    using myAspServer.Controller.Todo;
    using myAspServer.Model.Todo.Repository;
    using myAspServer.Model.Todo.Service;
    using NUnit.Framework;
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
        public void TestPost()
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
        public void TestGet()
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