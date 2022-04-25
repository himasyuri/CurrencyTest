namespace CurrencyWebService.Models.Responses
{
    public class CurrencyResponse
    {
        public int Pages { get; set; }

        public int CurrentPage { get; set; }

        public List<Currency> Currencies { get; set; } = new List<Currency>();
    }
}
