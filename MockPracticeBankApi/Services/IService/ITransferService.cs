using MockPracticeBankApi.Models;

namespace MockPracticeBankApi.Services.IService
{
    public interface ITransferService
    {
        public Task<TransferResponse> MakeTransfer(Guid CustomerId, int recipientAccountNumber, decimal amount);
    }
}
