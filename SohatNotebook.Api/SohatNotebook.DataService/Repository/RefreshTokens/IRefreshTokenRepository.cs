using SohatNotebook.Entities.DbSet;

namespace SohatNotebook.DataService.Repository.RefreshTokens
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshTokenDb>
    {
        Task<RefreshTokenDb> GetByRefreshToken(string refreshToken);
        Task<bool> MarkRefreshTokenAsUSed(RefreshTokenDb refreshToken);
    }
}
