using Stocker_API.Sales.Domain.Model.Aggregates;
using Stocker_API.Sales.Interfaces.REST.Resources;
using Stocker_API.Inventory.Interfaces.REST.Transform;

namespace Stocker_API.Sales.Interfaces.REST.Transform;

public static class SaleDetailResourceFromEntityAssembler
{
    public static SaleDetailResource ToResourceFromEntity(SaleDetail saleDetail)
    {
        return new SaleDetailResource(
            saleDetail.Id,
            SaleResourceFromEntityAssembler.ToResourceFromEntity(saleDetail.Sale),
            ProductResourceFromEntityAssembler.ToResourceFromEntity(saleDetail.Product),
            saleDetail.SalePrice,
            saleDetail.Quantity,
            saleDetail.Subtotal
            );
    }
}