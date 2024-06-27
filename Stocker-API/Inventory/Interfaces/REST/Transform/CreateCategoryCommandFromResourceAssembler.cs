using Stocker_API.Inventory.Domain.Model.Commands;
using Stocker_API.Inventory.Interfaces.REST.Resources;

namespace Stocker_API.Inventory.Interfaces.REST.Transform;

public static class CreateCategoryCommandFromResourceAssembler
{
    public static CreateCategoryCommand ToCommandFromResource(CreateCategoryResource resource)
    {
        return new CreateCategoryCommand(resource.Name);
    }
}