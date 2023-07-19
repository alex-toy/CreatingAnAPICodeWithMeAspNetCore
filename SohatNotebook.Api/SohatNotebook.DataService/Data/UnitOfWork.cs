using Microsoft.Extensions.Logging;
using SohatNotebook.DataService.Configuration;
using SohatNotebook.DataService.Repository.HealthDatas;
using SohatNotebook.DataService.Repository.RefreshTokens;
using SohatNotebook.DataService.Repository.Users;

namespace SohatNotebook.DataService.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<UserRepository> _userLogger;
        private readonly ILogger<HealthDataRepository> _healtDataLogger;
        private readonly ILogger<RefreshTokenRepository> _refreshTokenLogger;

        public IUserRepository Users { get; private set; }
        public IHealthDataRepository HealthDatas { get; private set; }

        public IRefreshTokenRepository RefreshTokens { get; private set; }

        public UnitOfWork(AppDbContext appDbContext,
                          ILogger<UserRepository> logger,
                          ILogger<HealthDataRepository> healtDataLogger,
                          ILogger<RefreshTokenRepository> refreshTokenLogger)
        {
            _appDbContext = appDbContext;
            _userLogger = logger;
            _healtDataLogger = healtDataLogger;
            _refreshTokenLogger = refreshTokenLogger;
            Users = new UserRepository(_appDbContext, _userLogger);
            HealthDatas = new HealthDataRepository(_appDbContext, _healtDataLogger);
            RefreshTokens = new RefreshTokenRepository(_appDbContext, _refreshTokenLogger);
        }

        public async Task CompleteAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
