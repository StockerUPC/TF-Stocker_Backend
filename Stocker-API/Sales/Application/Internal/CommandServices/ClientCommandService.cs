using Stocker_API.Sales.Domain.Model.Commands;
using Stocker_API.Sales.Domain.Model.Entities;
using Stocker_API.Sales.Domain.Repositories;
using Stocker_API.Sales.Domain.Services;
using Stocker_API.Shared.Domain.Repositories;

namespace Stocker_API.Sales.Application.Internal.CommandServices;

public class ClientCommandService(IClientRepository clientRepository, IUnitOfWork unitOfWork)
    : IClientCommandService
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
    public async Task<Client?> Handle(CreateClientCommand command)
    {
        var client = new Client(command.Name, command.Number, command.Email);
        await clientRepository.AddAsync(client);
        await unitOfWork.CompleteAsync();
        return client;
    }
    
    public async Task<Client?> Delete(Client client)
    {
        clientRepository.Remove(client);
        await unitOfWork.CompleteAsync();
        return client;
    }

    public async Task<Client?> Update(Client client)
    {
        clientRepository.Update(client);
        await unitOfWork.CompleteAsync();
        return client;
    }
}