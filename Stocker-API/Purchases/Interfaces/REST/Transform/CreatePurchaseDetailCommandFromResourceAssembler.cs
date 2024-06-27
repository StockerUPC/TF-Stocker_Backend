using Stocker_API.Purchases.Domain.Model.Commands;
using Stocker_API.Purchases.Interfaces.REST.Resources;

namespace Stocker_API.Purchases.Interfaces.REST.Transform;

public static class CreatePurchaseDetailCommandFromResourceAssembler
{
    public static CreatePurchaseDetailCommand ToCommandFromResource(CreatePurchaseDetailResource resource)
    {
        return new CreatePurchaseDetailCommand(resource.PurchaseId, resource.ProductId, resource.PurchasePrice, resource.SalePrice,resource.Quantity, resource.Total);
    }
}