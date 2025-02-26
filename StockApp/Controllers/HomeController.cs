using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockApp.Models;
using StockApp.Services;
using StockApp.ServicesContracts;

namespace StockApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFinnhubService _IfinnhubService;
        private readonly IOptions<TradingOptions> _tradingOptions;

        public HomeController(FinnhubService IfinnhubService , IOptions<TradingOptions> tradingoption)
        {
            _IfinnhubService = IfinnhubService;
            _tradingOptions = tradingoption;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(_tradingOptions.Value.DefaultStockSymbol))
            {
                _tradingOptions.Value.DefaultStockSymbol = "MSFT";
            }

            Dictionary<string, object> stockResponce = await _IfinnhubService.GetStockQuote(_tradingOptions.Value.DefaultStockSymbol);

            Stock stock = new Stock()
            {
                StockSymbol = _tradingOptions.Value.DefaultStockSymbol,
                CurrentPrice = Convert.ToDecimal(stockResponce["c"].ToString()),
                HighPriceOfTheDay = Convert.ToDecimal(stockResponce["h"].ToString()),
                LowPriceOfTheday = Convert.ToDecimal(stockResponce["l"].ToString()),
                OpenPriceOfTheDay = Convert.ToDecimal(stockResponce["o"].ToString()),
                PercentChange = Convert.ToDecimal(stockResponce["pc"].ToString()),
                PreviousClosePrice = Convert.ToDecimal(stockResponce["dp"].ToString())
            };


            return View(stock);
        }
    }
}
