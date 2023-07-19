using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SohatNotebook.DataService.Data;
using SohatNotebook.Entities.DbSet;

namespace SohatNotebook.DataService.Repository.HealthDatas
{
    public class HealthDataRepository : GenericRepository<HealthDataDb>, IHealthDataRepository
    {
        private readonly ILogger<HealthDataRepository> _logger;

        public HealthDataRepository(AppDbContext appDbContext, ILogger<HealthDataRepository> logger) : base(appDbContext)
        {
            _logger = logger;
        }

        public Task<bool> Add(HealthDataDb userDb)
        {
            _dbSet.Add(userDb);
            return Task.FromResult(true);
        }

        public Task<bool> Delete(Guid id, string userId)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<HealthDataDb>> GetAll()
        {
            try
            {
                List<HealthDataDb>? userDbs = _dbSet.Where(u => u.Status == 1).ToList();
                return userDbs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<HealthDataDb>();
            }
        }

        public Task<bool> Upsert(HealthDataDb entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(HealthDataDb userDb)
        {
            try
            {
                HealthDataDb? user = await _dbSet.FirstOrDefaultAsync(u => u.Status == 1 && u.Id == userDb.Id);
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

        private static void Map(HealthDataDb userDb, ref HealthDataDb user)
        {
            user.Height = userDb.Height;
            user.Weight = userDb.Weight;
            user.BloodType = userDb.BloodType;
            user.Race = userDb.Race;
            user.UseGlasses = userDb.UseGlasses;
        }
    }
}
