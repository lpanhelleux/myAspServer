namespace myAspServer.Controller.User
{
    using myAspServer.Model.User.Service;

    public static class UserControllerBuilder
    {
        public static UserController Build(IUserService userService)
        {
            return new UserController(userService);
        }
    }
}
