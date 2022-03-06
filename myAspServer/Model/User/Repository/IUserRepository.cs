namespace myAspServer.Model.User.Repository
{
    using myAspServer.Model.Common.Entity;
    using myAspServer.Model.User.Entity;

    public interface IUserRepository
    {
        void Add(UserEntity todoItem);
        Task<UserEntity?> Get(int id);
        Task<IList<UserEntity>> GetAll();
        Task<ITodoResult> Delete(int id);
        Task<ITodoResult> Update(int id, string? name);
    }
}
