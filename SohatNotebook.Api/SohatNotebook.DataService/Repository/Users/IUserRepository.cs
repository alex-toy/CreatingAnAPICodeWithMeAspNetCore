using SohatNotebook.Entities.DbSet;

namespace SohatNotebook.DataService.Repository.Users
{
    public interface IUserRepository : IGenericRepository<UserDb>
    {
        Task<UserDb> GetByEmail(string email);
        Task<bool> Update(UserDb userDb);
        Task<UserDb> GetByIdentityId(Guid id);
    }
}
