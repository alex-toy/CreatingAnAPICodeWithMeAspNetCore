using Microsoft.EntityFrameworkCore;
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

        public Task<bool> Add(UserDb userDb)
        {
            _dbSet.Add(userDb);
            return Task.FromResult(true);
        }

        public Task<bool> Delete(Guid id, string userId)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<UserDb>> GetAll()
        {
            try
            {
                List<UserDb>? userDbs = _dbSet.Where(u => u.Status == 1).ToList();
                return userDbs; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<UserDb>();
            }
        }

        public Task<UserDb> GetById(Guid userId)
        {
            UserDb? userDb = _appDbContext.Users.FirstOrDefault(u => u.Id == userId);
            return Task.FromResult(userDb); ;
        }

        public Task<UserDb> GetByEmail(string email)
        {
            try
            {
                Task<UserDb?> userDbs = _dbSet.FirstOrDefaultAsync(u => u.Status == 1 && u.Email == email);
                return userDbs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (Task<UserDb>)Enumerable.Empty<UserDb>();
            }
        }

        public Task<bool> Upsert(UserDb entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(UserDb userDb)
        {
            try
            {
                UserDb? user = await _dbSet.FirstOrDefaultAsync(u => u.Status == 1 && u.Id == userDb.Id);
                if (user == null) return false;
                Map(userDb, ref user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<UserDb> GetByIdentityId(Guid identityId)
        {
            try
            {
                UserDb? user = await _dbSet.FirstOrDefaultAsync(u => u.Status == 1 && u.Id == identityId);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        private static void Map(UserDb userDb, ref UserDb user)
        {
            user.FirstName = userDb.FirstName;
            user.LastName = userDb.LastName;
            user.MobileNumber = userDb.MobileNumber;
            user.Phone = userDb.Phone;
            user.Gender = userDb.Gender;
            user.Address = userDb.Address;
            user.UpdateDate = DateTime.UtcNow;
            user.Email = userDb.Email;
            user.DateOfBirth = userDb.DateOfBirth;
            user.Country = userDb.Country;
            user.Profession = userDb.Profession;
            user.Hobby = userDb.Hobby;
        }
    }
}
