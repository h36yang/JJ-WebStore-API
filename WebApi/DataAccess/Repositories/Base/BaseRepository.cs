using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess.Repositories
{
    public abstract partial class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase, new()
    {
        private bool _disposedValue;

        protected DbSet<TEntity> Table { get; private set; }

        protected WebStoreContext Context { get; private set; }

        protected BaseRepository(WebStoreContext context)
        {
            Context = context;
            Table = Context.Set<TEntity>();
        }

        public int Add(TEntity entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }

        public async Task<int> AddAsync(TEntity entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? await SaveChangesAsync() : 0;
        }

        public int AddRange(IEnumerable<TEntity> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? await SaveChangesAsync() : 0;
        }

        public int Count() => Table.Count();

        public async Task<int> CountAsync() => await Table.CountAsync();

        public int Delete(TEntity entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }

        public async Task<int> DeleteAsync(TEntity entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? await SaveChangesAsync() : 0;
        }

        public int Delete(int id, bool persist = true)
        {
            TEntity result = Get(id);
            if (result == null)
            {
                throw new ArgumentException("Id does not exist", nameof(id));
            }

            return Delete(result, persist);
        }

        public async Task<int> DeleteAsync(int id, bool persist = true)
        {
            TEntity result = Get(id);
            if (result == null)
            {
                throw new ArgumentException("Id does not exist", nameof(id));
            }

            return await DeleteAsync(result, persist);
        }

        public int DeleteRange(IEnumerable<TEntity> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public async Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? await SaveChangesAsync() : 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Exist(int id) => Table.Any(t => t.Id == id);

        public async Task<bool> ExistAsync(int id) => await Table.AnyAsync(t => t.Id == id);

        public bool Exist(Expression<Func<TEntity, bool>> predicate) => Table.Any(predicate);

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate) => await Table.AnyAsync(predicate);

        public List<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return FindQuery(predicate, includes).ToList();
        }

        public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return await FindQuery(predicate, includes).ToListAsync();
        }

        public IQueryable<TEntity> FindQuery(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = Table.Where(predicate);

            if (includes != null)
            {
                query = includes(query);
            }

            return query;
        }

        public TEntity First(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return FirstQuery(predicate, includes).First();
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return await FirstQuery(predicate, includes).FirstAsync();
        }

        public IQueryable<TEntity> FirstQuery(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = Table.Where(predicate);

            if (includes != null)
            {
                query = includes(query);
            }

            return query;
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = FirstOrDefaultQuery(predicate, includes).FirstOrDefault();
            return result ?? new TEntity();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            TEntity result = await FirstOrDefaultQuery(predicate, includes).FirstOrDefaultAsync();
            return await Task.FromResult(result ?? new TEntity());
        }

        public IQueryable<TEntity> FirstOrDefaultQuery(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = Table.Where(predicate);

            if (includes != null)
            {
                query = includes(query);
            }

            return query;
        }

        public TEntity Get(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return GetQuery(id, includes).SingleOrDefault() ?? new TEntity();
        }

        public async Task<TEntity> GetAsync(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            TEntity result = await GetQuery(id, includes).SingleOrDefaultAsync();
            return await Task.FromResult(result ?? new TEntity());
        }

        public IQueryable<TEntity> GetQuery(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = Table;
            query = query.Where(t => t.Id == id);

            if (includes != null)
            {
                query = includes(query);
            }

            return query;
        }

        public List<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return GetAllQuery(includes).ToList();
        }

        public async Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return await GetAllQuery(includes).ToListAsync();
        }

        public IQueryable<TEntity> GetAllQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = Table;
            if (includes != null)
            {
                query = includes(query);
            }

            return query;
        }

        public List<TEntity> GetPage(int page = 1, int count = 10, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            List<TEntity> results = GetPageQuery(page, count, includes).ToList();

            return results.Any() == false ? new List<TEntity>() : results;
        }

        public async Task<List<TEntity>> GetPageAsync(int page = 1, int count = 10, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            List<TEntity> results = await GetPageQuery(page, count, includes).ToListAsync();
            return await Task.FromResult(results.Any() ? results : new List<TEntity>());
        }

        public IQueryable<TEntity> GetPageQuery(int page = 1, int count = 10, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = Table.Skip(count * (page - 1)).Take(count);

            if (includes != null)
            {
                query = includes(query);
            }

            return query;
        }

        public (int totalCount, int totalPages) GetTotalPages(int count)
        {
            var totalCount = Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)count);
            return (totalCount, totalPages);
        }

        public async Task<(int totalCount, int totalPages)> GetTotalPagesAsync(int count)
        {
            var totalCount = await CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)count);
            return (totalCount, totalPages);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return SingleQuery(predicate, includes).Single();
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return await SingleQuery(predicate, includes).SingleAsync();
        }

        public IQueryable<TEntity> SingleQuery(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = Table.Where(predicate);

            if (includes != null)
            {
                query = includes(query);
            }

            return query;
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            return SingleOrDefaultQuery(predicate, includes).SingleOrDefault() ?? new TEntity();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            TEntity result = await SingleOrDefaultQuery(predicate, includes).SingleOrDefaultAsync();
            return await Task.FromResult(result ?? new TEntity());
        }

        public IQueryable<TEntity> SingleOrDefaultQuery(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = Table.Where(predicate);

            if (includes != null)
            {
                query = includes(query);
            }

            return query;
        }

        public int Update(TEntity entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }

        public async Task<int> UpdateAsync(TEntity entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? await SaveChangesAsync() : 0;
        }

        public int UpdateRange(IEnumerable<TEntity> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? await SaveChangesAsync() : 0;
        }

        public int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //A concurrency error occurred and should be handled intelligently
                Console.WriteLine(ex);
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                //DbResiliency retry limit exceeded and should be handled intelligently
                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                //A general exception occurred and should be handled intelligently
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //A concurrency error occurred and should be handled intelligently
                Console.WriteLine(ex);
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                //DbResiliency retry limit exceeded and should be handled intelligently
                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                //A general exception occurred and should be handled intelligently
                Console.WriteLine(ex);
                throw;
            }
        }

        private void Dispose(bool disposing)
        {
            if (_disposedValue)
            {
                return;
            }

            if (disposing)
            {
                Context.Dispose();
            }
            _disposedValue = true;
        }
    }
}
