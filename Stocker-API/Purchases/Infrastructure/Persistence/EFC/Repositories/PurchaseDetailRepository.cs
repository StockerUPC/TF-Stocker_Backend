using Stocker_API.Purchases.Domain.Model.Aggregates;
using Stocker_API.Purchases.Domain.Repositories;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Stocker_API.Purchases.Infrastructure.Persistence.EFC.Repositories;

public class PurchaseDetailRepository(AppDbContext context) : BaseRepository<PurchaseDetail>(context), IPurchaseDetailRepository
{
    /**
     * Find Tutorial By id
     * <summary>
     *     This method is used to find a tutorial by id, overriding the base method to include the category
     * </summary>
     * @param int id
     */
    public new async Task<PurchaseDetail?> FindByIdAsync(int id) =>
        await Context.Set<PurchaseDetail>().Include(s => s.Purchase)
            .Where(s => s.Id == id).FirstOrDefaultAsync();
    
    public new async Task<IEnumerable<PurchaseDetail>> ListAsync()
    {
        return await Context.Set<PurchaseDetail>()
            .Include(purchaseDetail =>purchaseDetail.Purchase)
            .ToListAsync();
    }

    public async Task<IEnumerable<PurchaseDetail>> FindByPurchaseIdAsync(int purchaseId)
    {
        return await Context.Set<PurchaseDetail>()
            .Include(purchaseDetail => purchaseDetail.Purchase)
            .Where(purchaseDetail => purchaseDetail.PurchaseId == purchaseId)
            .ToListAsync();
    }
}