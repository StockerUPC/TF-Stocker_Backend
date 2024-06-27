using Stocker_API.Purchases.Domain.Model.Commands;
using Stocker_API.Purchases.Interfaces.REST.Resources;

namespace Stocker_API.Purchases.Interfaces.REST.Transform;

public static class CreatePurchaseCommandFromResourceAssembler
{
    public static CreatePurchaseCommand ToCommandFromResource(CreatePurchaseResource resource)
    {
        return new CreatePurchaseCommand(resource.SupplierId, resource.TotalAmount);
    }
}