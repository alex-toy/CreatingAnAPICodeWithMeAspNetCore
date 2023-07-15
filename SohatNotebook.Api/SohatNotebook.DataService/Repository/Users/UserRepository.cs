using Microsoft.Extensions.Logging;
using SohatNotebook.DataService.Data;
using SohatNotebook.Entities.DbSet;

namespace SohatNotebook.DataService.Repository.Users
{
    public class UserRepository : GenericRepository<UserDb>, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(AppDbContext appDbContext, ILogger<UserRepository> logger) : base(appDbContext)
        {
            _logger = logger;
        }

        public Task<bool> Add(UserDb entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDb>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserDb> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Upsert(UserDb entity)
        {
            throw new NotImplementedException();
        }
    }
}
