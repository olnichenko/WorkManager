using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected WorkManagerDbContext _workManagerDbContext { get; set; }
        public RepositoryBase(WorkManagerDbContext workManagerDbContext)
        {
            _workManagerDbContext= workManagerDbContext;
        }
        public IQueryable<T> FindAll() => _workManagerDbContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _workManagerDbContext.Set<T>().Where(expression).AsNoTracking();
        public IQueryable<T> FindByConditionWithTracking(Expression<Func<T, bool>> expression) =>
            _workManagerDbContext.Set<T>().Where(expression);
        public void Create(T entity) => _workManagerDbContext.Set<T>().Add(entity);
        public void Update(T entity) => _workManagerDbContext.Set<T>().Update(entity);
        public void Delete(T entity) => _workManagerDbContext.Set<T>().Remove(entity);
        public async Task<T> FindAsync(long id) => await _workManagerDbContext.Set<T>().FindAsync(id);
    }
}
