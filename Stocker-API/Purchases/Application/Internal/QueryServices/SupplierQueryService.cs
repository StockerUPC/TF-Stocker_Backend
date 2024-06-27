using Stocker_API.Purchases.Domain.Model.Entities;
using Stocker_API.Purchases.Domain.Model.Queries;
using Stocker_API.Purchases.Domain.Repositories;
using Stocker_API.Purchases.Domain.Services;

namespace Stocker_API.Purchases.Application.Internal.QueryServices;

public class SupplierQueryService(ISupplierRepository supplierRepository) : ISupplierQueryService
{
    /**
     * <summary>
     *     This method is responsible for handling GetCategoryByIdQuery
     * </summary>
     * <param name="query">GetCategoryByIdQuery>Contains the Id of the Category</param>
     * <returns>Category - The Category object</returns>
     */
    public async Task<Supplier?> Handle(GetSupplierByIdQuery query)
    {
        return await supplierRepository.FindByIdAsync(query.Id);
    }

    /**
     * <summary>
     *     This method is responsible for handling GetAllCategoriesQuery
     * </summary>
     * <param name="query">GetAllCategoriesQuery</param>
     * <returns>Category - The Category object</returns>
     */
    public async Task<IEnumerable<Supplier>> Handle(GetAllSuppliersQuery query)
    {
        return await supplierRepository.ListAsync();
    }
}