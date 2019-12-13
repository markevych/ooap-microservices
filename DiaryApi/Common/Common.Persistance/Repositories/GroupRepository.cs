using Common.Domain.Interfaces.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Common.Persistence.Models;
using Common.Persistence.Contexts;

namespace Common.Persistence.Repositories
{
    public class GroupRepository : IRepository<Group>
    {
        DbContext context;
        DbSet<Group> dbSet;

        public GroupRepository(ApplicationContext context)
        {
            this.context = context;
            this.dbSet = context.Groups;
        }

        public IEnumerable<Group> Get()
        {
            return this.dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<Group> Get(Func<Group, bool> predicate)
        {
            return this.dbSet.AsNoTracking().Where(predicate);
        }

        public Group FindById(int id)
        {
            return this.dbSet.Find(id);
        }

        public void Create(Group item)
        {
            this.dbSet.Add(item);
            this.context.SaveChanges();
        }

        public void Update(Group item)
        {
            this.context.Entry(item).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void Remove(Group item)
        {
            this.dbSet.Remove(item);
            this.context.SaveChanges();
        }

        public IEnumerable<Group> GetWithInclude(params Expression<Func<Group, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<Group> GetWithInclude(Func<Group, bool> predicate,
            params Expression<Func<Group, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<Group> Include(params Expression<Func<Group, object>>[] includeProperties)
        {
            IQueryable<Group> query = this.dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
