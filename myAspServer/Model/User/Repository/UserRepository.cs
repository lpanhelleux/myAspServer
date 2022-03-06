namespace myAspServer.Model.User.Repository
{
    using Microsoft.EntityFrameworkCore;
    using myAspServer.Context.Database;
    using myAspServer.Model.Common.Entity;
    using myAspServer.Model.User.Entity;

    public class UserRepository : IUserRepository
    {
        public UserRepository(TodoDbContext dbContext)
        {
            DbContext = dbContext;
            DbContext.Database.EnsureCreated();
        }

        private TodoDbContext DbContext { get; }

        public async void Add(UserEntity user)
        {
            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();
        }

        public async Task<UserEntity?> Get(int id)
        {
            if (await DbContext.Users.FindAsync(id) is UserEntity user)
            {
                return user;
            }

            return null;
        }

        public async Task<IList<UserEntity>> GetAll()
        {
            return await DbContext.Users.ToListAsync();
        }

        public async Task<ITodoResult> Delete(int id)
        {
            if (await DbContext.Users.FindAsync(id) is UserEntity user)
            {
                DbContext.Users.Remove(user);
                await DbContext.SaveChangesAsync();
                return TodoResults.Ok(user);
            }

            return TodoResults.NotFound();
        }

        public async Task<ITodoResult> Update(int id, string? name)
        {
            var user = await DbContext.Users.FindAsync(id);

            if (user is null) return TodoResults.NotFound();

            user.Name = name;

            await DbContext.SaveChangesAsync();

            return TodoResults.NoContent();
        }

    }
}
