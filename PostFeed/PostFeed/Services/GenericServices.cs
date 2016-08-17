using PostFeed.Domain;
using PostFeed.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostFeed.Services
{
    public class GenericServices<T> where T : class, IDbEntity, IActivatable
    {
        internal GenericRepository<T> _repo;

        public T Get(int id)
        {
            var query = _repo.Get(id);
            return (from t in query select t).FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return _repo.GetAll();
        }

        public void Update(T alteredEntity)
        {
            _repo.Update(alteredEntity);
            _repo.SaveChanges();
        }

        public int Add(T newEntity)
        {
            return _repo.AddAndSave(newEntity);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
            _repo.SaveChanges();
        }

        public void Delete(T removeEntity)
        {
            _repo.Delete(removeEntity);
            _repo.SaveChanges();
        }

    }
}