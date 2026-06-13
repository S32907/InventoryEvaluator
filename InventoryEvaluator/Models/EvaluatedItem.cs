namespace InventoryEvaluator.Models;

public class EvaluatedItem
{
    public string Name { get; set; } = "";
    public int Amount { get; set; }  
    public double AvgSellPrice { get; set; } 
    public double HighestBuyPrice { get; set; }
    public double TotalSellPrice { get; set; }

    public override string ToString()
    {
        return $"Item Name: {Name}, Amount: {Amount}, Avg Sell Price: {AvgSellPrice}, Highest Buy Price: {HighestBuyPrice}, Total Sell Price: {TotalSellPrice}";
    }
}