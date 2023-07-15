using Microsoft.Extensions.Logging;
using SohatNotebook.DataService.Configuration;
using SohatNotebook.DataService.Repository.Users;

namespace SohatNotebook.DataService.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<UserRepository> _logger;

        public IUserRepository Users { get; private set; }

        public UnitOfWork(AppDbContext appDbContext, ILogger<UserRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            Users = new UserRepository(_appDbContext, _logger);
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
