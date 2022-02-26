namespace myAspServer.Model.Todo.Entity
{
    public class TodoItemEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
