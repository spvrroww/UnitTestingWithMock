namespace MockPracticeBankApi.Models
{
    public record BalanceResponse(bool isSuccessful, string? ErrorMessage = null, Decimal? Balance = null);

}
