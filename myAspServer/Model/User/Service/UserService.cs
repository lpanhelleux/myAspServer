namespace myAspServer.Model.User.Service
{
    using myAspServer.Model.Common.Entity;
    using myAspServer.Model.User.Entity;
    using myAspServer.Model.User.Repository;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        private IUserRepository UserRepository { get; }

        public void Create(UserEntity user) => UserRepository.Add(user);

        public UserEntity? Get(int id)
        {
            return UserRepository.Get(id).Result;
        }

        public IList<UserEntity> GetAll()
        {
            return UserRepository.GetAll().Result;
        }

        public ITodoResult Delete(int id)
        {
            return UserRepository.Delete(id).Result;
        }

        public ITodoResult Update(int id, string? name)
        {
            return UserRepository.Update(id, name).Result;
        }

    }
}
