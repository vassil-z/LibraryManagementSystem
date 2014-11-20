using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BaseRepository<T> where T : BaseEntityWithID
    {
        public DbContext Context { get; set; }
        protected IDbSet<T> DbSet { get; set; }
        public UnitOfWork UnitOfWork { get; set; }

        public BaseRepository(UnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentException("An instance of the unitOfWork is null", "unitOfWork");
            }

            this.Context = unitOfWork.Context;
            this.DbSet = this.Context.Set<T>();

            this.UnitOfWork = unitOfWork;
        }

        public BaseRepository(DbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        public virtual int Count(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public virtual List<T> GetAll(int startPage = 0, int itemsPerPage = 0, Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> order = null, string includeProperties = "")
        {
            IQueryable<T> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(
                new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (order != null && (startPage != 0 && itemsPerPage != 0))
            {
                query = order(query).Skip((startPage - 1) * itemsPerPage).Take(itemsPerPage);
            }

            if (order == null && (startPage != 0 && itemsPerPage != 0))
            {
                query = query.OrderByDescending(x => x.ID).Skip((startPage - 1) * itemsPerPage).Take(itemsPerPage);
            }

            return query.ToList();
        }

        public virtual T GetByID(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Delete(T item)
        {
            this.DbSet.Remove(item);
            this.Context.SaveChanges();
        }

        private void Insert(T item)
        {
            this.DbSet.Add(item);
            this.Context.SaveChanges();
        }

        private void Update(T item)
        {
            this.Context.Entry(item).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public virtual void Save(T item)
        {
            if (item.ID <= 0)
            {
                Insert(item);
            }
            else
            {
                Update(item);
            }
        }

        public virtual void Dispose()
        {
            if (this.Context != null)
            {
                this.Context.Dispose();
            }
        }
    }
}
