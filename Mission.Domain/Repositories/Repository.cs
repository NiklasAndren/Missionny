using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities;
using Mission.Domain.Entities.FakeData;
using Mission.Domain.Entities.Abstract;
using Mission.Domain.Repositories;
using Mission.Domain.Repositories.Abstract;
using System.Data.Entity;
using Mission.Domain.Contexts;

namespace Mission.Domain.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected DbContext _context;
        protected DbSet<T> _dbSet;

        public Repository()
        {
            _context = new EFDbContext();
            _dbSet = _context.Set<T>();
        }

        

        public DbContext Model
        {
            get { return _context; }
        }

        public virtual IQueryable<T> FindAll(Func<T, bool> filter = null)
        {
            if (null == filter)
                return _dbSet;
            return _dbSet.Where(filter).AsQueryable();
        }

        public virtual T FindByID(Guid id)
        {
            return _dbSet.FirstOrDefault(e => e.ID == id);
        }

        public virtual void Save(T entity)
        {
            var existing = _dbSet.Where(e => e.ID == entity.ID).FirstOrDefault();
            if (null != existing)
                _context.Entry(entity).State = System.Data.EntityState.Modified;
            else
                _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            _context.Entry(entity).State = System.Data.EntityState.Deleted;
            _context.SaveChanges();
        }

        /// <summary>
        /// To have any use for this method - _context.SaveChanges needs to be removed from .Save(..) and .Delete(..).
        /// After that change an entity is saved by e.g: "repo.Save(entity);repo.Commit();"
        ///
        /// Useful in case bulk operations or more complex transactions are needed.
        /// </summary>
        public virtual void Commit()
        {
            _context.SaveChanges();
        }
    }

     public interface IAppUserRepository : IRepository<User>
        {
        User GetUserNameByEmail(string email);
        void RegisterUser(User user);
        void DeleteUserByUserName(string username);
        }

     public class AppUserRepository : Repository<User>, IAppUserRepository
     {
         public AppUserRepository() : base() { }

         public User GetUserNameByEmail(string email)
         {
             return FindAll(u => u.UserEmailAddress == email).FirstOrDefault();
         }
         public void RegisterUser(User user)
         {
             user.ID = Guid.NewGuid();
             _dbSet.Add(user);
             _context.SaveChanges();
         }
         public void DeleteUserByUserName(string username)
         {
             var user = FindAll(u => u.UserName == username).FirstOrDefault();
             _dbSet.Remove(user);
             _context.SaveChanges();
         }
     }
}

