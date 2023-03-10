using MockPractice.Repository.IRepository;
using MockPracticeBankApi.Models;
using MockPracticeBankApi.Services.IService;

namespace MockPracticeBankApi.Services
{
    public class AccountVerificationService : IAccountVerificationService
    {
        private readonly IRepositoryManager _repositoryManager;

        public AccountVerificationService(IRepositoryManager repositoryManager)
        {
            _repositoryManager=repositoryManager;
        }

        public async Task<CustomerAccountDetails> VerifyAccountNumber(int AccountNumber)
        {
            if (AccountNumber < 100000000 || AccountNumber > 999999999) return  null;

            var account = (await _repositoryManager.BankAccountRepository.GetBy(x => x.AccountNo == AccountNumber.ToString().ToCharArray())).FirstOrDefault();
            if (account is null) return null;

            var customer = await _repositoryManager.CustomerRepository.Get(account.CustomerId);
            if(customer is null) throw new ArgumentNullException(nameof(customer), "Error thrown in VerifyAccount Number Operation due to a null customer value");

            return new CustomerAccountDetails(customer.FirstName, customer.LastName, int.Parse(account.AccountNo));

        }
    }
}
