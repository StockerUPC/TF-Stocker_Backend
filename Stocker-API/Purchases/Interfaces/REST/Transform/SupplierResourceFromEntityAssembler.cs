using Stocker_API.Purchases.Domain.Model.Entities;
using Stocker_API.Purchases.Interfaces.REST.Resources;

namespace Stocker_API.Purchases.Interfaces.REST.Transform;

public static class SupplierResourceFromEntityAssembler
{
    public static SupplierResource ToResourceFromEntity(Supplier entity)
    {
        Console.WriteLine("Supplier Name is " + entity.Name);
        return new SupplierResource(entity.Id, entity.Name, entity.Number, entity.Email);
    }
}