using Cytocol.Data.Context;
using Cytocol.Domain.Entities;
using Cytocol.Domain.Exceptions;
using Cytocol.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly CytocolDbContext _context;

        protected BaseRepository(CytocolDbContext context)
        {
            _context = context;
        }

        public virtual async Task<List<TEntity>> FindAll() => await _context.Set<TEntity>().ToListAsync();


        public virtual async Task<TEntity> FindById(int id) => await _context.Set<TEntity>().FindAsync(id);

        public virtual async Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> predicate) => await _context.Set<TEntity>()
            .FirstOrDefaultAsync(predicate);

        public virtual async Task<TEntity> Save(TEntity entity)
        {
            var _context = new CytocolDbContext();

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            _context.Set<TEntity>().AddOrUpdate(entity);
            await _context.SaveChangesAsync();
            return entity;
        }



        public async virtual Task<TSubEntity> GetSubEntityById<TSubEntity>(int Id) where TSubEntity : class, IsEntity
        {
            TSubEntity entity = await _context.Set<TSubEntity>().FindAsync(Id);
            if (entity == null || entity.IsDeleted) throw new GenericNotFoundException<TSubEntity>(Id);
            return entity;
        }

        public IQueryable<TEntity> FindAllQueryable()
        {
            throw new NotImplementedException();
        }
    }
}
