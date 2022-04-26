using Microsoft.AspNetCore.Mvc;
using CurrencyWebService.Services;
using CurrencyWebService.Models;
using CurrencyWebService.Models.Responses;

namespace CurrencyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [Route("Currencies/{page}")]
        [HttpGet]
        public ActionResult<List<Currency>> GetCurrencies(int page)
        {
            var currencies = _currencyService.GetCurrencyList();
            var pageResults = 4f;
            var pageCount = Math.Ceiling(currencies.Count / pageResults);
            var result = currencies.Skip((page - 1) * (int)pageResults)
                                   .Take((int)pageResults)
                                   .ToList();

            CurrencyResponse response = new CurrencyResponse()
            {
                Currencies = result,
                CurrentPage = page,
                Pages = (int)pageCount
            };
            return Ok(response);
        }

        [Route("Currency/{currCharCode}")]
        [HttpGet]
        public ActionResult GetCurrency(string currCharCode)
        {
            var result = _currencyService.GetCurrency(currCharCode);
            return Ok(result);
        }
    }
}
