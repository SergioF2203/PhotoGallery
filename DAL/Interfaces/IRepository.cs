using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity:BaseEntity
    {
        IQueryable<TEntity> FindAll();
        Task<TEntity> FindByIdAsync(string id);
        Task AddAsync(TEntity entity);
        void Add(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        void Remove(TEntity entity);

    }
}
