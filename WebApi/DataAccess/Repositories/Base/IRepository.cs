using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApi.DataAccess.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        int Add(TEntity entity, bool persist = true);

        Task<int> AddAsync(TEntity entity, bool persist = true);

        int AddRange(IEnumerable<TEntity> entities, bool persist = true);

        Task<int> AddRangeAsync(IEnumerable<TEntity> entities, bool persist = true);

        int Count();

        Task<int> CountAsync();

        int Delete(int id, bool persist = true);

        Task<int> DeleteAsync(int id, bool persist = true);

        int Delete(TEntity entity, bool persist = true);

        Task<int> DeleteAsync(TEntity entity, bool persist = true);

        int DeleteRange(IEnumerable<TEntity> entities, bool persist = true);

        Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities, bool persist = true);

        bool Exist(int id);

        Task<bool> ExistAsync(int id);

        bool Exist(Expression<Func<TEntity, bool>> predicate);

        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);

        List<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        IQueryable<TEntity> FindQuery(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        TEntity First(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        IQueryable<TEntity> FirstQuery(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        IQueryable<TEntity> FirstOrDefaultQuery(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        TEntity Get(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task<TEntity> GetAsync(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        IQueryable<TEntity> GetQuery(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        List<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        IQueryable<TEntity> GetAllQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        List<TEntity> GetPage(int page = 1, int count = 10, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task<List<TEntity>> GetPageAsync(int page = 1, int count = 10, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        IQueryable<TEntity> GetPageQuery(int page = 1, int count = 10, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        (int totalCount, int totalPages) GetTotalPages(int count);

        Task<(int totalCount, int totalPages)> GetTotalPagesAsync(int count);

        int SaveChanges();

        Task<int> SaveChangesAsync();

        TEntity Single(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        IQueryable<TEntity> SingleQuery(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        IQueryable<TEntity> SingleOrDefaultQuery(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        int Update(TEntity entity, bool persist = true);

        Task<int> UpdateAsync(TEntity entity, bool persist = true);

        int UpdateRange(IEnumerable<TEntity> entities, bool persist = true);

        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, bool persist = true);
    }
}
