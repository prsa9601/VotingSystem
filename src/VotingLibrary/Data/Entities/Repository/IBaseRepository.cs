﻿using System.Linq.Expressions;

namespace VotingLibrary.Data.Entities.Repository
{
    public interface IBaseRepository<T> where T : BaseClass
    {
        Task<T?> GetAsync(long id);
        Task<List<T>?> GetListTrackingAsync();
        Task<List<T>?> GetListByFilterAsync(Expression<Func<T, bool>> expression);
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> expression);

        Task<T?> GetTracking(Guid id);
        Task<T?> GetTrackingWithString(string id);

        Task AddAsync(T entity);
        void Add(T entity);

        Task AddRange(ICollection<T> entities);

        void Update(T entity);

        Task<int> Save();
        int SaveChange();

        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);

        bool Exists(Expression<Func<T, bool>> expression);

        Task<bool> Delete(Expression<Func<T, bool>> expression);
        Task<bool> DeleteOneEntity(Expression<Func<T, bool>> expression);
        Task<bool> DeleteAsync(T entity);

        T? Get(long id);
    }
}
