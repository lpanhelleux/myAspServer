﻿namespace myAspServer.Controller.Todo
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        public int? UserId  { get; set; }
    }
}
    