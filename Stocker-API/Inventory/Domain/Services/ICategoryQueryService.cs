using Stocker_API.Inventory.Domain.Model.Entities;
using Stocker_API.Inventory.Domain.Model.Queries;

namespace Stocker_API.Inventory.Domain.Services;

public interface ICategoryQueryService
{
    Task<Category?> Handle(GetCategoryByIdQuery query);
    Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery query);
    
    
}