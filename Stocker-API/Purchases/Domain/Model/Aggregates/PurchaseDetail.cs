using Stocker_API.Inventory.Domain.Model.Aggregates;
namespace Stocker_API.Purchases.Domain.Model.Aggregates;

public partial class PurchaseDetail
{
    public PurchaseDetail(int purchaseId, int productId, decimal purchasePrice, decimal salePrice, int quantity, decimal total)
    {
        PurchaseId = purchaseId;
        ProductId = productId;
        PurchasePrice = purchasePrice;
        SalePrice = salePrice;
        Quantity = quantity;
        Total = total;
    }

    public int Id { get; }
    public Purchase Purchase { get; internal set; }
    public Product Product { get; internal set; }
    public decimal PurchasePrice { get;  set; }
    public decimal SalePrice { get;  set; }
    public int Quantity { get;  set; }
    public decimal Total { get;  set; }
    public int PurchaseId { get;  set; }    
    public int ProductId { get;  set; }
}