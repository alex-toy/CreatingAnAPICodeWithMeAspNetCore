using SohatNotebook.DataService.Repository.Users;

namespace SohatNotebook.DataService.Configuration
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        Task CompleteAsync();
    }
}
