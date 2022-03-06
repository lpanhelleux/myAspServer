namespace myAspServer.Model.User.Service
{
    using myAspServer.Model.User.Repository;

    public static class UserServiceBuilder
    {
        public static IUserService Build(IUserRepository userRepository)
        {
            return new UserService(userRepository);
        }
    }
}
