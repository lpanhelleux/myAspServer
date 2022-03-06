namespace myAspServer.Model.User.Service
{
    using myAspServer.Model.Common.Entity;
    using myAspServer.Model.User.Entity;

    public interface IUserService
    {
        void Create(UserEntity user);
        UserEntity? Get(int id);
        IList<UserEntity> GetAll();
        ITodoResult Delete(int id);
        ITodoResult Update(int id, string? name);
    }
}
