namespace myAspServer.Model.User.Repository
{
    using myAspServer.Context.Database;

    public static class UserRepositoryBuilder
    {
        public static IUserRepository Build(TodoDbContext dbContext)
        {
            return new UserRepository(dbContext);
        }
    }
}
