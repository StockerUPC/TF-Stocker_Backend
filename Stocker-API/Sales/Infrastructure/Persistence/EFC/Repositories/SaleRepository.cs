using Stocker_API.Sales.Domain.Model.Aggregates;
using Stocker_API.Sales.Domain.Repositories;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Stocker_API.Sales.Infrastructure.Persistence.EFC.Repositories;

public class SaleRepository(AppDbContext context) : BaseRepository<Sale>(context), ISaleRepository
{
    /**
     * Find Tutorial By id
     * <summary>
     *     This method is used to find a tutorial by id, overriding the base method to include the category
     * </summary>
     * @param int id
     */
    public new async Task<Sale?> FindByIdAsync(int id) =>
        await Context.Set<Sale>().Include(s => s.Client)
            .Where(s => s.Id == id).FirstOrDefaultAsync();
    
    public new async Task<IEnumerable<Sale>> ListAsync()
    {
        return await Context.Set<Sale>()
            .Include(sale => sale.Client)
            .ToListAsync();
    }

    public async Task<IEnumerable<Sale>> FindByClientIdAsync(int clientId)
    {
        return await Context.Set<Sale>()
            .Include(sale => sale.Client)
            .Where(sale => sale.ClientId == clientId)
            .ToListAsync();
    }
}