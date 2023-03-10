using MockPractice.DataAccess;
using MockPractice.Domain;
using MockPractice.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockPractice.Repository
{
    public class BankAccountRepository : GenericRepository<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(MockDbContext db) : base(db)
        {
        }

        public async Task<BankAccount> GetByAccountNo(int accountNumber) => await GetSingleBy(x => x.AccountNo == accountNumber.ToString().ToCharArray());
    }
}
