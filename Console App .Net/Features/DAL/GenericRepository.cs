using Features.DbContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.DAL
{
    public class GenericRepository<T> where T : Features.Common.EntityBase
    {

        protected SchoolContext _context;
        protected readonly IDbSet<T> _dbset;

        protected GenericRepository(SchoolContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public IEnumerable<T> GetAllBy(Expression<Func<T, bool>> predicate)
        {
            var query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }
        public T Create(T entity)
        {
            var data = _dbset.Add(entity);
            _context.SaveChanges();
            return data;
        }
        public T Get(int id)
        {
            return _dbset.FirstOrDefault(e => e.Id == id);
        }
        public T GetBy(Expression<Func<T, bool>> predicate)
        {
            return _dbset.FirstOrDefault(predicate);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public T Delete(T entity)
        {
            var data = _dbset.Remove(entity);
            _context.SaveChanges();
            return data;
        }
    }
}
