namespace myAspServer.Controller.User
{
    using myAspServer.Controller.ControllerResults;
    using myAspServer.Model.Common.Entity;
    using myAspServer.Model.User.Entity;
    using myAspServer.Model.User.Service;

    public class UserController : IUserController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IControllerResult Post(UserDTO userDTO)
        {
            UserEntity userEntity = new()
            {
                Name = userDTO.Name
            };

            userService.Create(userEntity);

            return ControllerResults.Ok(Build(userEntity));
        }

        public IControllerResult Get(int id)
        {
            return userService.Get(id) is UserEntity userEntity
                ? ControllerResults.Ok(Build(userEntity))
                : ControllerResults.NotFound();
        }

        public IControllerResult GetAll()
        {
            IList<UserDTO> userDTOs = userService.GetAll().Select(x => Build(x)).ToList();
            return ControllerResults.Ok(userDTOs);
        }

        public IControllerResult Delete(int id)
        {
            ITodoResult result = userService.Delete(id);

            return result.Code == ITodoResultsEnum.NoContent
                ? ControllerResults.NoContent()
                : ControllerResults.NotFound();
        }

        public IControllerResult Put(int id, UserDTO userDTO)
        {
            ITodoResult todoItemResult = userService.Update(id, userDTO.Name);

            return todoItemResult.Code == ITodoResultsEnum.NoContent
                ? ControllerResults.NoContent()
                : ControllerResults.NotFound();
        }

        private static UserDTO Build(UserEntity userEntity)
        {
            return new UserDTO()
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
            };
        }
    }
}
