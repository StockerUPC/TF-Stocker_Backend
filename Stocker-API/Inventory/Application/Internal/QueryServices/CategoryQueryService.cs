using Stocker_API.Inventory.Domain.Model.Entities;
using Stocker_API.Inventory.Domain.Model.Queries;
using Stocker_API.Inventory.Domain.Repositories;
using Stocker_API.Inventory.Domain.Services;

namespace Stocker_API.Inventory.Application.Internal.QueryServices;

public class CategoryQueryService(ICategoryRepository categoryRepository) : ICategoryQueryService
{
    /**
     * <summary>
     *     This method is responsible for handling GetCategoryByIdQuery
     * </summary>
     * <param name="query">GetCategoryByIdQuery>Contains the Id of the Category</param>
     * <returns>Category - The Category object</returns>
     */
    public async Task<Category?> Handle(GetCategoryByIdQuery query)
    {
        return await categoryRepository.FindByIdAsync(query.Id);
    }

    /**
     * <summary>
     *     This method is responsible for handling GetAllCategoriesQuery
     * </summary>
     * <param name="query">GetAllCategoriesQuery</param>
     * <returns>Category - The Category object</returns>
     */
    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery query)
    {
        return await categoryRepository.ListAsync();
    }
}