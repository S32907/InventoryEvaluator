using System.Text.Json.Serialization;

namespace InventoryEvaluator.Models;

public class Item
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    public override string ToString()
    {
        return $"{Name}: {Amount}";
    }
}