using System.Text.Json.Serialization;

namespace InventoryEvaluator.Models;

public class Order
{
    [JsonPropertyName("type")]
    public string OrderType { get; set; } = "";
    [JsonPropertyName("platinum")]
    public int Price { get; set; }
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    [JsonPropertyName("visible")]
    public bool IsVisible { get; set; }
    [JsonPropertyName("user")]
    public User User { get; set; } = new();

    public override string ToString()
    {
        return $"type: {OrderType}, price: {Price}, quantity: {Quantity}, visible: {IsVisible}, {User}";
    }
}

public class User
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = "";
    [JsonPropertyName("ingameName")]
    public string Name { get; set; } = "";

    public override string ToString()
    {
        return $" user.status: {Status}, user.name: {Name}";
    }
}

public class Root
{
    [JsonPropertyName("data")]
    public List<Order> Data { get; set; } = [];
}