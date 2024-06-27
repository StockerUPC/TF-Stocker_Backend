using Stocker_API.Sales.Domain.Model.Aggregates;
using Stocker_API.Sales.Interfaces.REST.Resources;

namespace Stocker_API.Sales.Interfaces.REST.Transform;

public static class SaleResourceFromEntityAssembler
{
    public static SaleResource ToResourceFromEntity(Sale sale)
    {
        return new SaleResource(
            sale.Id,
            ClientResourceFromEntityAssembler.ToResourceFromEntity(sale.Client),
            sale.TotalAmount
            );
    }
}