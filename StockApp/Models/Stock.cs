using System.Runtime.InteropServices;

namespace StockApp.Models
{
    public class Stock
    {
        public string StockSymbol { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public decimal PercentChange { get; set; }
        public decimal HighPriceOfTheDay { get; set; }
        public decimal LowPriceOfTheday { get; set; }
        public decimal OpenPriceOfTheDay { get; set; }
        public decimal PreviousClosePrice { get; set; }

    }
}
