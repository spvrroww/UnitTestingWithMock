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
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(MockDbContext db) : base(db)
        {
        }
    }

}
