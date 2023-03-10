using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockPractice.Repository.IRepository
{
    public interface IRepositoryManager
    {
        public IBankAccountRepository BankAccountRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public Task<bool> SaveAsync();
        public void Rollback();
    }
}
