namespace CurrencyWebService.Models
{
    public class Currency
    {
        public string CharCode { get; set; } = string.Empty;

        public int Nominal { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Value { get; set; } = decimal.Zero;
    }
}
