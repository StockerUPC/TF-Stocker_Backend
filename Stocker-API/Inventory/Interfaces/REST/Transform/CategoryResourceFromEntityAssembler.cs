using Stocker_API.Inventory.Domain.Model.Entities;
using Stocker_API.Inventory.Interfaces.REST.Resources;

namespace Stocker_API.Inventory.Interfaces.REST.Transform;

public static class CategoryResourceFromEntityAssembler
{
    public static CategoryResource ToResourceFromEntity(Category entity)
    {
        Console.WriteLine("Category Name is " + entity.Name);
        return new CategoryResource(entity.Id, entity.Name);
    }
}