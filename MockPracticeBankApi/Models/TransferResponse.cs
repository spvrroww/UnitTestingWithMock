using MockPractice.Domain;

namespace MockPracticeBankApi.Models
{
    public record TransferResponse(bool IsSuccessful, string? ErrorMessage = null);
}
