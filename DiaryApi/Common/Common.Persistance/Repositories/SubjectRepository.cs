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
    public class SubjectRepository : IRepository<Subject>
    {
        DbContext context;
        DbSet<Subject> dbSet;

        public SubjectRepository(ApplicationContext context)
        {
            this.context = context;
            this.dbSet = context.Subjects;
        }

        public IEnumerable<Subject> Get()
        {
            return this.dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<Subject> Get(Func<Subject, bool> predicate)
        {
            return this.dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public Subject FindById(int id)
        {
            return this.dbSet.Find(id);
        }

        public void Create(Subject item)
        {
            this.dbSet.Add(item);
            this.context.SaveChanges();
        }

        public void Update(Subject item)
        {
            this.context.Entry(item).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void Remove(Subject item)
        {
            this.dbSet.Remove(item);
            this.context.SaveChanges();
        }

        public IEnumerable<Subject> GetWithInclude(params Expression<Func<Subject, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<Subject> GetWithInclude(Func<Subject, bool> predicate,
            params Expression<Func<Subject, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<Subject> Include(params Expression<Func<Subject, object>>[] includeProperties)
        {
            IQueryable<Subject> query = this.dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
