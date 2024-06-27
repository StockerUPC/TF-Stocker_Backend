using Stocker_API.Sales.Domain.Model.Commands;
using Stocker_API.Sales.Interfaces.REST.Resources;

namespace Stocker_API.Sales.Interfaces.REST.Transform;

public static class CreateClientCommandFromResourceAssembler
{
    public static CreateClientCommand ToCommandFromResource(CreateClientResource resource)
    {
        return new CreateClientCommand(resource.Name, resource.Number, resource.Email);
    }
}