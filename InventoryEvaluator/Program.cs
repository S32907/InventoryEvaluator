using System.Text.Json;
using InventoryEvaluator.Models;
using InventoryEvaluator.Services;

var evaluationService = new EvaluationService();
evaluationService.GetItemsFromJson();
await evaluationService.EvaluateItems();

foreach (var item in evaluationService.EvaluatedItems)
{
    Console.WriteLine(item);
}
Console.WriteLine("Total items Price: " + (int)evaluationService.GetTotalSellPrice());
  