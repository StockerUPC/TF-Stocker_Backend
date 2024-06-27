using Stocker_API.Inventory.Domain.Model.Commands;
using Stocker_API.Inventory.Interfaces.REST.Resources;

namespace Stocker_API.Inventory.Interfaces.REST.Transform;

public static class CreateProductCommandFromResourceAssembler
{
    public static CreateProductCommand ToCommandFromResource(CreateProductResource resource)
    {
        return new CreateProductCommand(resource.Name, resource.Description, resource.CategoryId, resource.PhotoUrl, resource.PurchasePrice, resource.SalePrice, resource.Stock, resource.ExpiryDate);
    }
}