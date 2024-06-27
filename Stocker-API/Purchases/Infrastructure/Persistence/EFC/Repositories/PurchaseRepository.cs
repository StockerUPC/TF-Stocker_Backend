using Stocker_API.Purchases.Domain.Model.Aggregates;
using Stocker_API.Purchases.Domain.Repositories;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Stocker_API.Purchases.Infrastructure.Persistence.EFC.Repositories;

public class PurchaseRepository(AppDbContext context) : BaseRepository<Purchase>(context), IPurchaseRepository
{
    /**
     * Find Tutorial By id
     * <summary>
     *     This method is used to find a tutorial by id, overriding the base method to include the category
     * </summary>
     * @param int id
     */
    public new async Task<Purchase?> FindByIdAsync(int id) =>
        await Context.Set<Purchase>().Include(s => s.Supplier)
            .Where(s => s.Id == id).FirstOrDefaultAsync();
    
    public new async Task<IEnumerable<Purchase>> ListAsync()
    {
        return await Context.Set<Purchase>()
            .Include(purchase => purchase.Supplier)
            .ToListAsync();
    }

    public async Task<IEnumerable<Purchase>> FindBySupplierIdAsync(int supplierId)
    {
        return await Context.Set<Purchase>()
            .Include(purchase => purchase.Supplier)
            .Where(purchase => purchase.SupplierId == supplierId)
            .ToListAsync();
    }
}