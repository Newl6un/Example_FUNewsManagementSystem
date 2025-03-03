using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LeQuangTrungMVC.DataAccessObjects
{
    public abstract class GenericDAO<T> where T : class
    {
        protected FunewsManagementContext Context { get; set; }

        protected GenericDAO(FunewsManagementContext funewsManagementContext)
        {
            Context = funewsManagementContext;
        }

        public IQueryable<T> FindAll()
        => Context.Set<T>()
             .AsNoTracking();


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        => Context.Set<T>()
            .Where(expression)
            .AsNoTracking();

        public async Task Create(T entity)
        {
            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();
            Context.ChangeTracker.Clear();
        }
        public async Task Update(T entity)
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            Context.ChangeTracker.Clear();
        }
        public async Task Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
            Context.ChangeTracker.Clear();
        }

    }
}
