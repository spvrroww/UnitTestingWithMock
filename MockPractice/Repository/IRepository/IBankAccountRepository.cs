using MockPractice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockPractice.Repository.IRepository
{
    public interface IBankAccountRepository: IGenericRepository<BankAccount>
    {
        public Task<BankAccount> GetByAccountNo(int accountNumber);
    }
}
