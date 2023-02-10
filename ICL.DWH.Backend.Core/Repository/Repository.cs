using ICL.DWH.Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly DataContext _dataContext;
        protected readonly DbSet<T> DbSet;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
            DbSet = _dataContext.Set<T>();
        }

        public T Create(T entity)
        {
            DbSet.Add(entity);
            _dataContext.SaveChanges();
            return entity;
        }

        public IQueryable<T> GetAll()
        {
            return DbSet.AsNoTracking().AsQueryable();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public T Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dataContext.Entry(entity).State = EntityState.Modified;
            _dataContext.SaveChanges();
            return entity;
        }

        public T GetById(Guid id)
        {
            return DbSet.SingleOrDefault(x => x.Id == id);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataContext.Dispose();
            }
        }
    }
}
