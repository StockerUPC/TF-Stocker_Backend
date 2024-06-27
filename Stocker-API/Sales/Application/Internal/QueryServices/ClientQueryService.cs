using Stocker_API.Sales.Domain.Model.Entities;
using Stocker_API.Sales.Domain.Model.Queries;
using Stocker_API.Sales.Domain.Repositories;
using Stocker_API.Sales.Domain.Services;

namespace Stocker_API.Sales.Application.Internal.QueryServices;

public class ClientQueryService(IClientRepository clientRepository) : IClientQueryService
{
    /**
     * <summary>
     *     This method is responsible for handling GetCategoryByIdQuery
     * </summary>
     * <param name="query">GetCategoryByIdQuery>Contains the Id of the Category</param>
     * <returns>Category - The Category object</returns>
     */
    public async Task<Client?> Handle(GetClientByIdQuery query)
    {
        return await clientRepository.FindByIdAsync(query.Id);
    }

    /**
     * <summary>
     *     This method is responsible for handling GetAllCategoriesQuery
     * </summary>
     * <param name="query">GetAllCategoriesQuery</param>
     * <returns>Category - The Category object</returns>
     */
    public async Task<IEnumerable<Client>> Handle(GetAllClientsQuery query)
    {
        return await clientRepository.ListAsync();
    }
}