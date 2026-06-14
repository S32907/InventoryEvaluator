using System.Text.Json;
using InventoryEvaluator.Models;


namespace InventoryEvaluator.Services;

public class EvaluationService
{
    private List<Item> Items { get; set; } = [];
    public List<EvaluatedItem> EvaluatedItems { get; set; } = [];
    private static readonly string Data =
        Path.Combine("..", "..", "..", "Data", "data.json");
    private static readonly HttpClient Client = new();
    private readonly SemaphoreSlim _rateLimiter = new SemaphoreSlim(3);

    public void GetItemsFromJson()
    {
        var json = File.ReadAllText(Data);
        Items = JsonSerializer.Deserialize<List<Item>>(json) ?? [];
    }

    public async Task EvaluateItems()
    {
        //clear previously stored statistic
        EvaluatedItems.Clear();
        
        var tasks = Items.Select(async item =>
            {
                await _rateLimiter.WaitAsync(); 
                try
                {
                    await EvaluateItem(item.Name, item.Amount);
                    await Task.Delay(1000);
                } catch(Exception e)
                {
                    Console.WriteLine($"Error occured for item {item.Name}: {e.Message}");
                }
                finally
                {
                    _rateLimiter.Release();
                }
            }
        );
        await  Task.WhenAll(tasks);
    }

    private async Task EvaluateItem(string itemName, int amount)
    {
        var url = $"https://api.warframe.market/v2/orders/item/{itemName}";
        
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Add("Accept", "application/json");
        
        //request item orders
        var response = await Client.GetAsync(url);
        
        response.EnsureSuccessStatusCode();
        
        var json = await response.Content.ReadAsStringAsync();
        //deserialize data into corresponding models
        var result = JsonSerializer.Deserialize<Root>(json);
        var orders = result?.Data ?? [];
        
        //get current top 5 sellers by lowest price
        var topSellers = orders.
            Where(o =>  o.User.Status.Equals("ingame") && o.OrderType.Equals("sell") && o.Price > 1).
            OrderBy(o => o.Price).
            Take(5).
            ToList();
        //get current top buyer
        var topBuyer = orders.Where(o => o.User.Status.Equals("ingame") && o.OrderType.Equals("buy") && o.Price > 1)
            .OrderByDescending(o => o.Price).FirstOrDefault();

        if (topSellers.Count == 0)
        {
            Console.WriteLine("No sell orders found for item: " + itemName + ". Cant evaluate item");
            return;
        }
        
        var averageSellPrice = (int)Math.Round(topSellers.Average(o => o.Price));
        var buyPrice = topBuyer?.Price ?? 0;
        
        //create evaluated items
        var evaluatedItem = new EvaluatedItem()
        {
            Name = itemName,
            Amount = amount,
            AvgSellPrice = averageSellPrice,
            HighestBuyPrice = buyPrice,
            TotalSellPrice = amount * averageSellPrice,
        };
        
        EvaluatedItems.Add(evaluatedItem);
    }

    public double GetTotalSellPrice()
    {
        return EvaluatedItems.Sum(o => o.TotalSellPrice);
    }
    
}