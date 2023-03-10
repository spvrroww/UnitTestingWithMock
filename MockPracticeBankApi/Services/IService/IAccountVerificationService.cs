using MockPracticeBankApi.Models;

namespace MockPracticeBankApi.Services.IService
{
    public interface IAccountVerificationService
    {
        public Task<CustomerAccountDetails> VerifyAccountNumber(int AccountNumber);
    }
}
