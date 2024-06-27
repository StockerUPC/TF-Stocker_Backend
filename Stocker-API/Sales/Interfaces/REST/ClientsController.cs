using System.Net.Mime;
using Stocker_API.Sales.Domain.Model.Queries;
using Stocker_API.Sales.Domain.Services;
using Stocker_API.Sales.Interfaces.REST.Resources;
using Stocker_API.Sales.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Stocker_API.Sales.Interfaces.REST;

/**
 * Categories Controller
 * <summary>
 *     This controller is responsible for handling all the requests related to categories.
 * </summary>
 */
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ClientsController(
    IClientCommandService clientCommandService,
    IClientQueryService clientQueryService)
    : ControllerBase
{
    /**
     * Create Category.
     * <summary>
     *     This method is responsible for handling the request to create a new category.
     * </summary>
     * <param name="createCategoryResource">The resource containing the information to create a new category.</param>
     * <returns>The newly created category resource.</returns>
     */
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a Client",
        Description = "Creates a Client with a given name",
        OperationId = "CreateClient")]
    [SwaggerResponse(201, "The category was created", typeof(ClientResource))]
    public async Task<IActionResult> CreateClient([FromBody] CreateClientResource createClientResource)
    {
        var createClientCommand =
            CreateClientCommandFromResourceAssembler.ToCommandFromResource(createClientResource);
        var client = await clientCommandService.Handle(createClientCommand);
        if (client is null) return BadRequest();
        var resource = ClientResourceFromEntityAssembler.ToResourceFromEntity(client);
        return CreatedAtAction(nameof(GetClientById), new { clientId = resource.Id }, resource);
    }

    /**
     * Get Category By Id.
     * <summary>
     *     This method is responsible for handling the request to get a category by its id.
     * </summary>
     * <param name="categoryId">The category identifier.</param>
     * <returns>The category resource.</returns>
     */
    [HttpGet("{clientId:int}")]
    [SwaggerOperation(
        Summary = "Gets a Client by id",
        Description = "Gets a Client for a given identifier",
        OperationId = "GetClientById")]
    [SwaggerResponse(200, "The category was found", typeof(ClientResource))]
    public async Task<IActionResult> GetClientById(int clientId)
    {
        var getClientByIdQuery = new GetClientByIdQuery(clientId);
        var client = await clientQueryService.Handle(getClientByIdQuery);
        var resource = ClientResourceFromEntityAssembler.ToResourceFromEntity(client);
        return Ok(resource);
    }

    /**
     * Get All Categories.
     * <summary>
     *     This method is responsible for handling the request to get all categories.
     * </summary>
     * <returns>The categories resources.</returns>
     */
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all Clients",
        Description = "Gets all Clients",
        OperationId = "GetAllClients")]
    [SwaggerResponse(200, "The Clients were found", typeof(IEnumerable<ClientResource>))]
    public async Task<IActionResult> GetAllClients()
    {
        var getAllClientsQuery = new GetAllClientsQuery();
        var clients = await clientQueryService.Handle(getAllClientsQuery);
        var resources = clients.Select(ClientResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpDelete("{clientId:int}")]
    [SwaggerOperation(
        Summary = "Deletes a Client by id",
        Description = "Deletes a Client for a given identifier",
        OperationId = "DeleteClientById")]
    [SwaggerResponse(200, "The client was found", typeof(ClientResource))]
    public async Task<IActionResult> DeleteClientById(int clientId)
    {
        var getClientByIdQuery = new GetClientByIdQuery(clientId);
        var client = await clientQueryService.Handle(getClientByIdQuery);
        await clientCommandService.Delete(client);
        return Ok(client);
    }
    
    [HttpPut("{clientId:int}")]
    [SwaggerOperation(
        Summary = "Updates a Client by id",
        Description = "Updates a Client for a given identifier",
        OperationId = "UpdateClientById")]
    [SwaggerResponse(200, "The category was found", typeof(ClientResource))]
    public async Task<IActionResult> UpdateClientById(int clientId, [FromBody] ClientResource clientResource)
    {
        var getClientByIdQuery = new GetClientByIdQuery(clientId);
        var client = await clientQueryService.Handle(getClientByIdQuery);
        client.Name = clientResource.Name;
        client.Number = clientResource.Number;
        client.Email = clientResource.Email;
        await clientCommandService.Update(client);
        return Ok(client);
    }
}