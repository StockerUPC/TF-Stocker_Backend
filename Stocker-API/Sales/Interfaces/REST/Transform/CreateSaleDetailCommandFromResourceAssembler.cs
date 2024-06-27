using Stocker_API.Sales.Domain.Model.Commands;
using Stocker_API.Sales.Interfaces.REST.Resources;

namespace Stocker_API.Sales.Interfaces.REST.Transform;

public static class CreateSaleDetailCommandFromResourceAssembler
{
    public static CreateSaleDetailCommand ToCommandFromResource(CreateSaleDetailResource resource)
    {
        return new CreateSaleDetailCommand(resource.SaleId, resource.ProductId, resource.SalePrice,resource.Quantity, resource.Subtotal);
    }
}