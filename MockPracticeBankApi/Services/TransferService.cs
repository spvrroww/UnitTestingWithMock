using MockPractice.Repository.IRepository;
using MockPracticeBankApi.Models;
using MockPracticeBankApi.Services.IService;

namespace MockPracticeBankApi.Services
{
    public class TransferService : ITransferService
    {
        private readonly IRepositoryManager _repositoryManager;

        public TransferService(IRepositoryManager repositoryManager)
        {
            _repositoryManager=repositoryManager;
        }

        public async Task<TransferResponse> MakeTransfer(Guid CustomerId, int recipientAccountNumber, decimal amount)
        {
            if (amount < 1) return new TransferResponse(false, "Invalid Input Amount");
            if (recipientAccountNumber < 100000000 || recipientAccountNumber > 999999999) return new TransferResponse(false, "Invalid Account Number");

            var reciepientAccount = await _repositoryManager.BankAccountRepository.GetByAccountNo(recipientAccountNumber);
            if (reciepientAccount is null) return new TransferResponse(false, "reciepient account number is invalid, confirm and try again");

            var sendersAccount = await _repositoryManager.BankAccountRepository.Get(CustomerId);
            if (sendersAccount is null) return new TransferResponse(false, "Customer does not have an active bank account");
            if (sendersAccount.Balance< amount ) return new TransferResponse(false, "Transfer failed due to insufficient funds");


            reciepientAccount.Balance += amount;

            var updatedReciepientAccount = await _repositoryManager.BankAccountRepository.Update(reciepientAccount);
            if (reciepientAccount is null) throw new ArgumentNullException("TransferService", "failed to update reciepient's account in make transfer method");

            sendersAccount.Balance -= amount;

            var updatedSendersAccount = await _repositoryManager.BankAccountRepository.Update(sendersAccount);
            if (updatedSendersAccount is null) throw new ArgumentNullException("TransferService", "failed to update sender's's account in make transfer method");

            if (await _repositoryManager.SaveAsync())
            {
                return new TransferResponse(true);
            }
            else
            {
                throw new OperationCanceledException("Make Tansfer Operation Failed");
            }
        }
    }
}
