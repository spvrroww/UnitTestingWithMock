using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MockPractice.Repository.IRepository
{
    public interface IGenericRepository<T>
    {
        public Task<T> Add(T entity);
        public Task<T> Update(T entity);
        public void Delete(T entity);
        public Task<List<T>> GetAll();
        public Task<T>Get(Guid id);
        public Task<List<T>> GetBy(Expression<Func<T, bool>> expression);
        public Task<T> GetSingleBy(Expression<Func<T, bool>> expression);
    }
}
