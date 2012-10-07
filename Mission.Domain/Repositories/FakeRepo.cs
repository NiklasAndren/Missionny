using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities;
using Mission.Domain.Repositories.Abstract;
using Mission.Domain.Entities.FakeData;
using Mission.Domain.Entities.Abstract;
using System.Data.Entity;



namespace Mission.Domain.Repositories
{
    public class FakeRepo<T> : IRepository<T> where T : class, IEntity
    {
        protected List<T> _dataSet;

        public FakeRepo(List<T> dataSet)
        {
            _dataSet = dataSet;
        }     

        public DbContext Model { get { return null; } }

        public virtual IQueryable<T> FindAll(Func<T,bool> filter = null)
        {
            if (null == filter)
                return _dataSet.AsQueryable();
            return _dataSet.Where(filter).AsQueryable();
        }

        public virtual T FindByID(Guid id)
        {
            return _dataSet.Find(e => e.ID == id);
        }

        public virtual void Save(T entity)
        {
            var existing = _dataSet.Where(e => e.ID == entity.ID).FirstOrDefault();
            if (null != existing)
                _dataSet[_dataSet.IndexOf(existing)] = entity;
            else
                _dataSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _dataSet.Remove(entity);
        }

        public virtual void Commit() { }

        
    }
}
