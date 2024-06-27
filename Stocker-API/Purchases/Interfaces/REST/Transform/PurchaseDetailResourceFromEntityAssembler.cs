using Stocker_API.Purchases.Domain.Model.Aggregates;
using Stocker_API.Purchases.Interfaces.REST.Resources;
using Stocker_API.Inventory.Interfaces.REST.Transform;

namespace Stocker_API.Purchases.Interfaces.REST.Transform;

public static class PurchaseDetailResourceFromEntityAssembler
{
    public static PurchaseDetailResource ToResourceFromEntity(PurchaseDetail purchaseDetail)
    {
        return new PurchaseDetailResource(
            purchaseDetail.Id,
            PurchaseResourceFromEntityAssembler.ToResourceFromEntity(purchaseDetail.Purchase),
            ProductResourceFromEntityAssembler.ToResourceFromEntity(purchaseDetail.Product),
            purchaseDetail.PurchasePrice,
            purchaseDetail.SalePrice,
            purchaseDetail.Quantity,
            purchaseDetail.Total
            );
    }
}