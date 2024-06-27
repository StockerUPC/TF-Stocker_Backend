using Stocker_API.Inventory.Domain.Model.Aggregates;
using Stocker_API.Inventory.Domain.Repositories;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Stocker_API.Inventory.Infrastructure.Persistence.EFC.Repositories;

public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
{
    /**
     * Find Tutorial By id
     * <summary>
     *     This method is used to find a tutorial by id, overriding the base method to include the category
     * </summary>
     * @param int id
     */
    public new async Task<Product?> FindByIdAsync(int id) =>
        await Context.Set<Product>().Include(p => p.Category)
            .Where(p => p.Id == id).FirstOrDefaultAsync();
    
    public new async Task<IEnumerable<Product>> ListAsync()
    {
        return await Context.Set<Product>()
            .Include(product => product.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> FindByCategoryIdAsync(int categoryId)
    {
        return await Context.Set<Product>()
            .Include(product => product.Category)
            .Where(product => product.CategoryId == categoryId)
            .ToListAsync();
    }
}