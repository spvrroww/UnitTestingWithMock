using Microsoft.EntityFrameworkCore;
using MockPractice.DataAccess;
using MockPractice.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MockPractice.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MockDbContext _db;

        public GenericRepository(MockDbContext db)
        {
            _db=db;
        }

        public async Task<T> Add(T entity) => (await _db.Set<T>().AddAsync(entity)).Entity;

        public async void Delete(T entity) => _db.Set<T>().Remove(entity);

        public async Task<T> Get(Guid id) => await _db.Set<T>().FindAsync(id);
      
        public async Task<List<T>> GetAll() => await _db.Set<T>().ToListAsync();
       
        public async Task< List<T>> GetBy(Expression<Func<T, bool>> expression) => await  _db.Set<T>().Where(expression).ToListAsync();

        public async Task<T> GetSingleBy(Expression<Func<T, bool>> expression) => await _db.Set<T>().SingleOrDefaultAsync(expression);

        public async Task<T> Update(T entity) => (_db.Set<T>().Update(entity)).Entity;

        
    }
}
