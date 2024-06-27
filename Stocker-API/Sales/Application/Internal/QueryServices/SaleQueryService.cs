using Stocker_API.Sales.Domain.Model.Aggregates;
using Stocker_API.Sales.Domain.Model.Queries;
using Stocker_API.Sales.Domain.Repositories;
using Stocker_API.Sales.Domain.Services;

namespace Stocker_API.Sales.Application.Internal.QueryServices;

public class SaleQueryService(ISaleRepository saleRepository) : ISaleQueryService
{
    /**
     * <summary>
     *     This method is responsible for handling GetTutorialByIdentifierQuery
     * </summary>
     * <param name="query">GetTutorialByIdentifierQuery>Contains the Id of the Tutorial</param>
     * <returns>Tutorial - The Tutorial object</returns>
     */
    public async Task<Sale?> Handle(GetSaleByIdQuery query)
    {
        return await saleRepository.FindByIdAsync(query.SaleId);
    }

    /**
     * <summary>
     *     This method is responsible for handling GetAllTutorialsQuery
     * </summary>
     * <param name="query">GetAllTutorialsQuery</param>
     * <returns>IEnumerable of Tutorials - The list of Tutorial objects</returns>
     */
    public async Task<IEnumerable<Sale>> Handle(GetAllSalesQuery query)
    {
        return await saleRepository.ListAsync();
    }
    
    /**
     * <summary>
     *     This method is responsible for handling GetAllTutorialsByCategoryIdQuery
     * </summary>
     * <param name="query">GetAllTutorialsByCategoryIdQuery</param>
     * <returns>IEnumerable of Tutorials - The list of Tutorial objects</returns>
     */
    public async Task<IEnumerable<Sale>> Handle(GetAllSalesByClientIdQuery query)
    {
        return await saleRepository.FindByClientIdAsync(query.ClientId);
    }
}