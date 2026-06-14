namespace InventoryEvaluator.Models;

public class EvaluatedItem
{
    public string Name { get; set; } = "";
    public int Amount { get; set; }  
    public int AvgSellPrice { get; set; } 
    public int HighestBuyPrice { get; set; }
    public int TotalSellPrice { get; set; }

    public override string ToString()
    {
        return $"Item Name: {Name}, Amount: {Amount}, Avg Sell Price: {AvgSellPrice}, Highest Buy Price: {HighestBuyPrice}, Total Sell Price: {TotalSellPrice}";
    }
}