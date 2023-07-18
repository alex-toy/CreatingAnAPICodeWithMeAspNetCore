using SohatNotebook.DataService.Repository.RefreshTokens;
using SohatNotebook.DataService.Repository.Users;
using SohatNotebook.Entities.DbSet;

namespace SohatNotebook.DataService.Configuration
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRefreshTokenRepository RefreshTokens { get; }

        Task CompleteAsync();
    }
}
