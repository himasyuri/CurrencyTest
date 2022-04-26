using CurrencyWebService.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Xml.Linq;

namespace CurrencyWebService.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IMemoryCache _memoryCache;

        public CurrencyService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Currency GetCurrency(string currCharCode)
        {
            XDocument xml = GetDocument();
            if (xml != null)
            {
                Currency currency = new Currency()
                {
                    CharCode = xml.Elements("ValCurs")?.Elements("Valute")
                                  .FirstOrDefault(x => x.Element("CharCode")?.Value == currCharCode)
                                  ?.Elements("CharCode").FirstOrDefault()?.Value,
                    Nominal = Convert.ToInt32(xml.Elements("ValCurs")?.Elements("Valute")
                                                 .FirstOrDefault(x => x.Element("CharCode")?.Value == currCharCode)
                                                 ?.Elements("Nominal").FirstOrDefault()?.Value),
                    Name = xml.Elements("ValCurs")?.Elements("Valute")
                              .FirstOrDefault(x => x.Element("CharCode")?.Value == currCharCode)
                              ?.Elements("Name").FirstOrDefault()?.Value,
                    Value = Convert.ToDecimal(xml.Elements("ValCurs")?.Elements("Valute")
                                                 .FirstOrDefault(x => x.Element("CharCode")?.Value == currCharCode)
                                                 ?.Elements("Value").FirstOrDefault()?.Value)
                };
                return currency;
            }
            throw new NotImplementedException();
        }

        public List<Currency> GetCurrencyList()
        {
            List<Currency> currencyList = new List<Currency>();
            XDocument xml = GetDocument();
            if (xml != null)
            {
                foreach (var item in xml.Elements("ValCurs").Elements("Valute"))
                {
                    Currency currency = new Currency()
                    {
                        CharCode = item?.Element("CharCode")?.Value,
                        Nominal = Convert.ToInt32(item?.Element("Nominal")?.Value),
                        Name = item?.Element("Name")?.Value,
                        Value = Convert.ToDecimal(item?.Element("Value")?.Value)
                    };
                    currencyList.Add(currency);
                }
                return currencyList;
            }
            throw new NotImplementedException();
        }

        private XDocument GetDocument()
        {
            string uri = "https://www.cbr-xml-daily.ru/daily_utf8.xml";
            XDocument doc;
            if (!_memoryCache.TryGetValue("currency-doc", out doc))
            {
                doc = XDocument.Load(uri);
                if (doc != null)
                {
                    _memoryCache.Set("currency-doc", doc, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
                }
            }
            return doc;
        }

    }
}
