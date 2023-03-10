using MockPractice.Domain;

namespace MockPracticeBankApi.Models
{
    public record DepositResponse(bool IsSuccessful, string? ErrorMessage = null, BankAccount? BankAccount = null);
}
