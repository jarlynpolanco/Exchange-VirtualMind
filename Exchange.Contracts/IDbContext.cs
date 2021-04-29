﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace Exchange.Contracts
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;

        EntityEntry<T> Entry<T>(T entity) where T : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        void Dispose();
    }
}
