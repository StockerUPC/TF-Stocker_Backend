using Stocker_API.Inventory.Domain.Model.Aggregates;
using Stocker_API.Inventory.Domain.Model.Commands;
using Stocker_API.Inventory.Domain.Repositories;
using Stocker_API.Inventory.Domain.Services;
using Stocker_API.Shared.Domain.Repositories;

namespace Stocker_API.Inventory.Application.Internal.CommandServices;

public class ProductCommandService(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : IProductCommandService
{

    public async Task<Product?> Handle(CreateProductCommand command)
    {
        var product = new Product(command.Name, command.Description, command.CategoryId, command.PhotoUrl, command.PurchasePrice, command.SalePrice, command.Stock, command.ExpiryDate);
        await productRepository.AddAsync(product);
        await unitOfWork.CompleteAsync();
        var category = await categoryRepository.FindByIdAsync(command.CategoryId);
        product.Category = category;
        return product;
    }   
    
    public async Task<Product?> Delete(Product product)
    {
        productRepository.Remove(product);
        await unitOfWork.CompleteAsync();
        return product;
    }
    
    public async Task<Product?> Update(Product product)
    {
        productRepository.Update(product);
        await unitOfWork.CompleteAsync();
        return product;
    }
}