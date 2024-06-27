using Stocker_API.Inventory.Domain.Model.Commands;
using Stocker_API.Inventory.Domain.Model.Entities;

namespace Stocker_API.Inventory.Domain.Services;

public interface ICategoryCommandService
{
    public Task<Category?> Handle(CreateCategoryCommand command);
    public Task<Category?> Delete(Category category);
    public Task<Category?> Update(Category category);
}