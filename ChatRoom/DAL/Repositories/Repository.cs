using ChatRoom.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ChatRoom.DAL.Repositories
{
    public class Repository<TEntity> : IDisposable where TEntity : class
    {
        protected ApplicationDbContext _context;

        protected readonly DbSet<TEntity> entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            entities.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entities.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            entities.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            return await entities.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await entities.Where(predicate).ToListAsync();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }



        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}


