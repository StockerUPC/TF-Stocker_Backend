using Stocker_API.Inventory.Domain.Model.Entities;
using Stocker_API.Sales.Domain.Model.Aggregates;

namespace Stocker_API.Inventory.Domain.Model.Aggregates;

public partial class Product
{
    public Product(string name, string description, int categoryId, string photoUrl, decimal purchasePrice, decimal salePrice, int stock, DateOnly expiryDate)
    {
        Name = name;
        Description = description;
        CategoryId = categoryId;
        PhotoUrl = photoUrl;
        PurchasePrice = purchasePrice;
        SalePrice = salePrice;
        Stock = stock;
        ExpiryDate = expiryDate;
    }

    public int Id { get; }
    public string Name { get;  set; }

    public string Description { get;  set; }

    public Category Category { get; internal set; }
    
    public string PhotoUrl { get;  set; }
    public decimal PurchasePrice { get;  set; }
    public decimal SalePrice { get;  set; }
    public int Stock { get;  set; }
    public DateOnly ExpiryDate { get;  set; }
    public int CategoryId { get;  set; }    
    public ICollection<SaleDetail> SaleDetails { get; }
}