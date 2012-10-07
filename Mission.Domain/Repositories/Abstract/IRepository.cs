using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;
using System.Data.Entity;

namespace Mission.Domain.Repositories.Abstract
{
    public interface IRepository<T> where T : IEntity
    {
        DbContext Model { get; }

        IQueryable<T> FindAll(Func<T, bool> filter = null);
        T FindByID(Guid id);
        void Save(T entity);
        void Delete(T entity);

        void Commit();
    }
}
