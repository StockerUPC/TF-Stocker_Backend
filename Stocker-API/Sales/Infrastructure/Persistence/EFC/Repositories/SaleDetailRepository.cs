using Stocker_API.Sales.Domain.Model.Aggregates;
using Stocker_API.Sales.Domain.Repositories;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Stocker_API.Sales.Infrastructure.Persistence.EFC.Repositories;

public class SaleDetailRepository(AppDbContext context) : BaseRepository<SaleDetail>(context), ISaleDetailRepository
{
    /**
     * Find Tutorial By id
     * <summary>
     *     This method is used to find a tutorial by id, overriding the base method to include the category
     * </summary>
     * @param int id
     */
    public new async Task<SaleDetail?> FindByIdAsync(int id) =>
        await Context.Set<SaleDetail>().Include(s => s.Sale)
            .Where(s => s.Id == id).FirstOrDefaultAsync();
    
    public new async Task<IEnumerable<SaleDetail>> ListAsync()
    {
        return await Context.Set<SaleDetail>()
            .Include(saleDetail => saleDetail.Sale)
            .ToListAsync();
    }

    public async Task<IEnumerable<SaleDetail>> FindBySaleIdAsync(int saleId)
    {
        return await Context.Set<SaleDetail>()
            .Include(saleDetail => saleDetail.Sale)
            .Where(saleDetail => saleDetail.SaleId == saleId)
            .ToListAsync();
    }
}