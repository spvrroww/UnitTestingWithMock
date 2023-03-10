using MockPracticeBankApi.Models;

namespace MockPracticeBankApi.Services.IService
{
    public interface IDepositService
    {
        public Task<DepositResponse> MakeDeposit(Guid id, decimal amount);
    }
}
