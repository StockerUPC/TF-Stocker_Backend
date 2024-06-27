using Stocker_API.Sales.Domain.Model.Commands;
using Stocker_API.Sales.Interfaces.REST.Resources;

namespace Stocker_API.Sales.Interfaces.REST.Transform;

public static class CreateSaleCommandFromResourceAssembler
{
    public static CreateSaleCommand ToCommandFromResource(CreateSaleResource resource)
    {
        return new CreateSaleCommand(resource.ClientId, resource.TotalAmount);
    }
}