using MockPractice.DataAccess;
using MockPractice.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockPractice.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly MockDbContext _db;
        private IBankAccountRepository _bankAccountRepository;
        private ICustomerRepository _customerRepository;


        public RepositoryManager(MockDbContext db)
        {
            _db=db;
        }

        public IBankAccountRepository BankAccountRepository => _bankAccountRepository??(_bankAccountRepository = new BankAccountRepository(_db));

        public ICustomerRepository CustomerRepository => _customerRepository??(_customerRepository = new CustomerRepository(_db));

        public void Rollback() => GC.SuppressFinalize(this);

        public async Task<bool> SaveAsync() => await _db.SaveChangesAsync() > 0;
        
    }
}
