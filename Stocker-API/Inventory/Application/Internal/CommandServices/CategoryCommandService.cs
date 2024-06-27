using Stocker_API.Inventory.Domain.Model.Commands;
using Stocker_API.Inventory.Domain.Model.Entities;
using Stocker_API.Inventory.Domain.Repositories;
using Stocker_API.Inventory.Domain.Services;
using Stocker_API.Shared.Domain.Repositories;

namespace Stocker_API.Inventory.Application.Internal.CommandServices;

public class CategoryCommandService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : ICategoryCommandService
{
    public async Task<Category?> Handle(CreateCategoryCommand command)
    {
        var category = new Category(command.Name);
        await categoryRepository.AddAsync(category);
        await unitOfWork.CompleteAsync();
        return category;
    }

    public async Task<Category?> Delete(Category category)
    {
        categoryRepository.Remove(category);
        await unitOfWork.CompleteAsync();
        return category;
    }

    public async Task<Category?> Update(Category category)
    {
        categoryRepository.Update(category);
        await unitOfWork.CompleteAsync();
        return category;
    }
    
}