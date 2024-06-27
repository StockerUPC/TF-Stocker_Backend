using Stocker_API.Purchases.Domain.Model.Commands;
using Stocker_API.Purchases.Interfaces.REST.Resources;

namespace Stocker_API.Purchases.Interfaces.REST.Transform;

public static class CreateSupplierCommandFromResourceAssembler
{
    public static CreateSupplierCommand ToCommandFromResource(CreateSupplierResource resource)
    {
        return new CreateSupplierCommand(resource.Name, resource.Number, resource.Email);
    }
}