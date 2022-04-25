using CurrencyWebService.Models;

namespace CurrencyWebService.Services
{
    public interface ICurrencyService
    {
        Currency GetCurrency(string currencyCode);

        List<Currency> GetCurrencyList();
    }
}
