using Stocker_API.Sales.Domain.Model.Aggregates;
using Stocker_API.Sales.Domain.Model.Commands;
using Stocker_API.Sales.Domain.Repositories;
using Stocker_API.Sales.Domain.Services;
using Stocker_API.Shared.Domain.Repositories;

namespace Stocker_API.Sales.Application.Internal.CommandServices;

public class SaleCommandService(ISaleRepository saleRepository, IClientRepository clientRepository, IUnitOfWork unitOfWork)
    : ISaleCommandService
{
    
    public async Task<Sale?> Handle(CreateSaleCommand command)
    {
        var sale = new Sale(command.ClientId, command.TotalAmount);
        await saleRepository.AddAsync(sale);
        await unitOfWork.CompleteAsync();
        var client = await clientRepository.FindByIdAsync(command.ClientId);
        sale.Client = client;
        return sale;
    }  
    public async Task<Sale?> Delete(Sale sale)
    {
        saleRepository.Remove(sale);
        await unitOfWork.CompleteAsync();
        return sale;
    }
    
    public async Task<Sale?> Update(Sale sale)
    {
        saleRepository.Update(sale);
        await unitOfWork.CompleteAsync();
        return sale;
    }
}