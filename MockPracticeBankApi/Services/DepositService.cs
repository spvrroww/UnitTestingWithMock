using MockPractice.Domain;
using MockPractice.Repository.IRepository;
using MockPracticeBankApi.Models;
using MockPracticeBankApi.Services.IService;

namespace MockPracticeBankApi.Services
{
    public class DepositService : IDepositService
    {
        private readonly IRepositoryManager _repositoryManager;

        public DepositService(IRepositoryManager repositoryManager)
        {
            _repositoryManager=repositoryManager;
        }

        public async Task<DepositResponse> MakeDeposit(Guid customerId, decimal amount)
        {
            if (amount < 0) return new DepositResponse(false, "Invalid input amount");

            var customerbankAccountList = await _repositoryManager.BankAccountRepository.GetBy(x=>x.CustomerId == customerId);
            if (customerbankAccountList is null || !customerbankAccountList.Any()) return new DepositResponse(false, "Customer does not have an active bank account");

            var customerBankAccount = customerbankAccountList.First();

            customerBankAccount.Balance += amount;
            customerBankAccount = await _repositoryManager.BankAccountRepository.Update(customerBankAccount);

            if(await _repositoryManager.SaveAsync())
            {
                return new DepositResponse(true, null, customerBankAccount);
            }
            else
            {
                throw new OperationCanceledException("Make Deposit Operation Failed");
            }

        }
    }
}
