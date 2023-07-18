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
                List<RefreshTokenDb>? refreshTokenDbs = _dbSet.Where(u => u.Status == 1).ToList();
                return refreshTokenDbs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<RefreshTokenDb>();
            }
        }

        public async Task<RefreshTokenDb> GetByRefreshToken(string refreshToken)
        {
            try
            {
                RefreshTokenDb? refreshTokenDb = _dbSet.FirstOrDefault(u => u.Token.ToLower() == refreshToken.ToLower());
                return refreshTokenDb;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> MarkRefreshTokenAsUSed(RefreshTokenDb refreshToken)
        {
            try
            {
                RefreshTokenDb? refreshTokenDb = _dbSet.FirstOrDefault(u => u.Token.ToLower() == refreshToken.Token.ToLower());
                if (refreshTokenDb == null) return false;
                refreshTokenDb.IsUsed = true;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
