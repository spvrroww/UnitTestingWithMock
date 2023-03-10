using MockPracticeBankApi.Models;

namespace MockPracticeBankApi.Services.IService
{
    public interface IBalanceService
    {
        public Task<BalanceResponse> CheckBalance(Guid CustomerId);
    }
}
