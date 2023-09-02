using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using System;
using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.Repositories.Interfaces;

namespace STAGGI_Budget_API.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected BudgetContext RepositoryContext { get; set; }
        public RepositoryBase(BudgetContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }
        public IQueryable<T> FindAll()
        {
            //return this.RepositoryContext.Set<T>().AsNoTracking();
            return this.RepositoryContext.Set<T>().AsNoTrackingWithIdentityResolution();
        }
        public IQueryable<T> FindAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> queryable = this.RepositoryContext.Set<T>();

            if (includes != null)
            {
                queryable = includes(queryable);
            }
            return queryable.AsNoTrackingWithIdentityResolution();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression).AsNoTrackingWithIdentityResolution();
        }
        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }
        public void SaveChanges()
        {
            this.RepositoryContext.SaveChanges();
        }

        public void Execute(string query)
        {
            this.RepositoryContext.Database.ExecuteSqlRaw(query);
        }
    }
}
