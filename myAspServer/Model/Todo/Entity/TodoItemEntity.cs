namespace myAspServer.Model.Todo.Entity
{
    using myAspServer.Model.User.Entity;

    public class TodoItemEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        public int? UserId { get; set; }
        public UserEntity? User { get; set; }
    }
}
