﻿using SohatNotebook.Entities.DbSet;

namespace SohatNotebook.DataService.Repository.Users
{
    public interface IUserRepository : IGenericRepository<UserDb>
    {
        Task<UserDb> GetByEmail(string email);
    }
}