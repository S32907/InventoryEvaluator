using System.Text.Json;
using InventoryEvaluator.Models;
using InventoryEvaluator.Services;
using InventoryEvaluator.Visual;

namespace InventoryEvaluator
{
    internal static class Program
    {
        [STAThread] 
        private static async Task Main()
        {
            var evaluationService = new EvaluationService();
            evaluationService.GetItemsFromJson();
            await evaluationService.EvaluateItems();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ResultFrame(evaluationService)); 
        }
    }
}




  