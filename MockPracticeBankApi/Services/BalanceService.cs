using MockPractice.Repository.IRepository;
using MockPracticeBankApi.Models;
using MockPracticeBankApi.Services.IService;

namespace MockPracticeBankApi.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IRepositoryManager _repositoryManager;

        public BalanceService(IRepositoryManager repositoryManager)
        {
            _repositoryManager=repositoryManager;
        }

        public async Task<BalanceResponse> CheckBalance(Guid CustomerId)
        {
            var bankAccount = (await _repositoryManager.BankAccountRepository.GetBy(x => x.CustomerId.Equals(CustomerId))).FirstOrDefault();
            if (bankAccount is null) return new BalanceResponse(false, "customer does not have an active bank account");

            return new BalanceResponse(true, null, bankAccount.Balance);
        }
    }
}
