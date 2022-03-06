using myAspServer.Controller.ControllerResults;

namespace myAspServer.Controller.User
{
    public interface IUserController
    {
        IControllerResult Post(UserDTO userJohn);
        IControllerResult Get(int id);
        IControllerResult GetAll();
        IControllerResult Delete(int id);
        IControllerResult Put(int id, UserDTO userDTO);
    }
}
