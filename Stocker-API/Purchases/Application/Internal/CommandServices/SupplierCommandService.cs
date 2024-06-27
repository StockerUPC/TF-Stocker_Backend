using Stocker_API.Purchases.Domain.Model.Commands;
using Stocker_API.Purchases.Domain.Model.Entities;
using Stocker_API.Purchases.Domain.Repositories;
using Stocker_API.Purchases.Domain.Services;
using Stocker_API.Shared.Domain.Repositories;

namespace Stocker_API.Purchases.Application.Internal.CommandServices;

public class SupplierCommandService(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork)
    : ISupplierCommandService
{
    /**
     * Create Category Command Handler
     * <summary>
     *     This method is responsible for handling the command and executing the business logic for creating a category.
     *     It is also responsible for calling the repository to persist the data.
     * </summary>
     * <param name="command">The command containing the name for the category</param>
     * <returns>The category entity.</returns>
     */
    public async Task<Supplier?> Handle(CreateSupplierCommand command)
    {
        var supplier = new Supplier(command.Name, command.Number, command.Email);
        await supplierRepository.AddAsync(supplier);
        await unitOfWork.CompleteAsync();
        return supplier;
    }
    
    public async Task<Supplier?> Delete(Supplier supplier)
    {
        supplierRepository.Remove(supplier);
        await unitOfWork.CompleteAsync();
        return supplier;
    }

    public async Task<Supplier?> Update(Supplier supplier)
    {
        supplierRepository.Update(supplier);
        await unitOfWork.CompleteAsync();
        return supplier;
    }
}