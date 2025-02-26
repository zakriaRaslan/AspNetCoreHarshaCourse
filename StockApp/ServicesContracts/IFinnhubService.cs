namespace StockApp.ServicesContracts
{
    public interface IFinnhubService
    {
        Task<Dictionary<string, object>> GetStockQuote(string stockSymbol);
    }
}
