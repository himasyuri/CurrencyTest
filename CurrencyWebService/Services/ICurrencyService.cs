using CurrencyWebService.Models;

namespace CurrencyWebService.Services
{
    public interface ICurrencyService
    {
        Currency GetCurrency(string currCharCode);

        List<Currency> GetCurrencyList();
    }
}
