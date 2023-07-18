using Microsoft.Extensions.Logging;
using SohatNotebook.DataService.Data;
using SohatNotebook.Entities.DbSet;

namespace SohatNotebook.DataService.Repository.RefreshTokens
{
    public class RefreshTokenRepository : GenericRepository<RefreshTokenDb>, IRefreshTokenRepository
    {
        private readonly ILogger<RefreshTokenRepository> _logger;

        public RefreshTokenRepository(AppDbContext appDbContext, ILogger<RefreshTokenRepository> logger) : base(appDbContext)
        {
            _logger = logger;
        }

        public override async Task<IEnumerable<RefreshTokenDb>> GetAll()
        {
            try
            {
                List<RefreshTokenDb>? userDbs = _dbSet.Where(u => u.Status == 1).ToList();
                return userDbs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<RefreshTokenDb>();
            }
        }
    }
}
