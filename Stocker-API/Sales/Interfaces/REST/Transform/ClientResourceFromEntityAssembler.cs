using Stocker_API.Sales.Domain.Model.Entities;
using Stocker_API.Sales.Interfaces.REST.Resources;

namespace Stocker_API.Sales.Interfaces.REST.Transform;

public static class ClientResourceFromEntityAssembler
{
    public static ClientResource ToResourceFromEntity(Client entity)
    {
        Console.WriteLine("Client Name is " + entity.Name);
        return new ClientResource(entity.Id, entity.Name, entity.Number, entity.Email);
    }
}