using Stocker_API.Purchases.Domain.Model.Aggregates;
using Stocker_API.Purchases.Interfaces.REST.Resources;

namespace Stocker_API.Purchases.Interfaces.REST.Transform;

public static class PurchaseResourceFromEntityAssembler
{
    public static PurchaseResource ToResourceFromEntity(Purchase purchase)
    {
        return new PurchaseResource(
            purchase.Id,
            SupplierResourceFromEntityAssembler.ToResourceFromEntity(purchase.Supplier),
            purchase.TotalAmount
            );
    }
}