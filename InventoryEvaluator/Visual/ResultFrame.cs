using System.Diagnostics.CodeAnalysis;
using InventoryEvaluator.Models;
using InventoryEvaluator.Services;

namespace InventoryEvaluator.Visual;
using System.Windows.Forms;

public class ResultFrame: Form
{
    private readonly EvaluationService _evaluationService;
    private DataGridView table;
    private Label totalLabel;
    private Button updateButton;
    public ResultFrame(EvaluationService evaluationService)
    {
        _evaluationService = evaluationService;
        Text = "Inventory Evaluation";
        Width = 1000;
        Height = 700;
        StartPosition = FormStartPosition.CenterScreen;
        
        var mainLabel = new Label()
        {
            Text = "Inventory",
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.MiddleCenter,
            Height = 40,
            Font = new Font("Segoe UI", 14, FontStyle.Bold)
        };
        
        //table for data
        table = new DataGridView()
        {
            Dock = DockStyle.Fill,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };
        table.Columns.Add("ItemName", "Item Name");
        table.Columns.Add("Amount", "Amount");
        table.Columns.Add("AvgSell", "Avg Sell");
        table.Columns.Add("Buy", "Top Buy");
        table.Columns.Add("TotalSell", "Total");
        
        //fill table with records
        FillTable();

        var bottomPanel = new Panel()
        {
            Dock = DockStyle.Bottom,
            Height = 50
        };

        totalLabel = new Label()
        {
            Text = $"Potential Inventory Value: {evaluationService.GetTotalSellPrice()}",
            AutoSize = true,
            Location =  new Point(10, 15),
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
        };
        
        updateButton = new Button
        {
            Text = "Update",
            Width = 100,
            Location = new Point(bottomPanel.Width - 110, 10),
            Anchor = AnchorStyles.Top | AnchorStyles.Right
        };
        updateButton.Click += updateButton_Click;
        
        bottomPanel.Controls.Add(totalLabel);
        bottomPanel.Controls.Add(updateButton);
        
        Controls.Add(table);
        Controls.Add(bottomPanel);
        Controls.Add(mainLabel);
        
        FormClosed += (sender, e) => Application.Exit();
    }

    private void FillTable()
    {
        table.Rows.Clear();
        
        foreach (var item in _evaluationService.EvaluatedItems)
        {
            table.Rows.Add(item.Name,item.Amount, item.AvgSellPrice, item.HighestBuyPrice, item.TotalSellPrice);
        }
    }

    private async void updateButton_Click(object? sender, EventArgs e)
    {
        updateButton.Enabled = false;
        updateButton.Text = "Updating...";
        
        await _evaluationService.EvaluateItems();
        FillTable();
        this.totalLabel.Text = $"Potential Inventory Value: {_evaluationService.GetTotalSellPrice()}";
        
        updateButton.Enabled = true;
        updateButton.Text = "Update";
    }
}