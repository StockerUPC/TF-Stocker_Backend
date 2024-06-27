using System.Net.Mime;
using Stocker_API.Profiles.Domain.Model.Queries;
using Stocker_API.Profiles.Domain.Services;
using Stocker_API.Profiles.Interfaces.REST.Resources;
using Stocker_API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Stocker_API.Profiles.Interfaces.REST;

/**
 * Subscriptions Controller
 * <summary>
 *     This controller is responsible for handling all the requests related to categories.
 * </summary>
 */
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class SubscriptionsController(
    ISubscriptionCommandService subscriptionCommandService,
    ISubscriptionQueryService subscriptionQueryService)
    : ControllerBase
{
    /**
     * Create Subscription.
     * <summary>
     *     This method is responsible for handling the request to create a new category.
     * </summary>
     * <param name="createCategoryResource">The resource containing the information to create a new category.</param>
     * <returns>The newly created category resource.</returns>
     */
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a Subscription",
        Description = "Creates a Subscription with a given name",
        OperationId = "CreateSubscription")]
    [SwaggerResponse(201, "The Subscription was created", typeof(SubscriptionResource))]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionResource createSubscriptionResource)
    {
        var createSubscriptionCommand =
            CreateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(createSubscriptionResource);
        var subscription = await subscriptionCommandService.Handle(createSubscriptionCommand);
        if (subscription is null) return BadRequest();
        var resource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return CreatedAtAction(nameof(GetSubscriptionById), new { subscriptionId = resource.Id }, resource);
    }

    /**
     * Get Category By Id.
     * <summary>
     *     This method is responsible for handling the request to get a category by its id.
     * </summary>
     * <param name="categoryId">The category identifier.</param>
     * <returns>The category resource.</returns>
     */
    [HttpGet("{subscriptionId:int}")]
    [SwaggerOperation(
        Summary = "Gets a Subscription by id",
        Description = "Gets a Subscription for a given identifier",
        OperationId = "GetSubscriptionById")]
    [SwaggerResponse(200, "The Subscription was found", typeof(SubscriptionResource))]
    public async Task<IActionResult> GetSubscriptionById(int subscriptionId)
    {
        var getSubscriptionByIdQuery = new GetSubscriptionByIdQuery(subscriptionId);
        var subscription = await subscriptionQueryService.Handle(getSubscriptionByIdQuery);
        var resource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
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
        Summary = "Gets all Subscriptions",
        Description = "Gets all Subscriptions",
        OperationId = "GetAllSubscriptions")]
    [SwaggerResponse(200, "The Subscriptions were found", typeof(IEnumerable<SubscriptionResource>))]
    public async Task<IActionResult> GetAllSubscriptions()
    {
        var getAllSubscriptionsQuery = new GetAllSubscriptionsQuery();
        var subscriptions = await subscriptionQueryService.Handle(getAllSubscriptionsQuery);
        var resources = subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpDelete("{subscriptionId:int}")]
    [SwaggerOperation(
        Summary = "Deletes a Subscription by id",
        Description = "Deletes a Subscription for a given identifier",
        OperationId = "DeleteSubscriptionById")]
    [SwaggerResponse(200, "The Subscription was found", typeof(SubscriptionResource))]
    public async Task<IActionResult> DeleteSubscriptionById(int subscriptionId)
    {
        var getSubscriptionByIdQuery = new GetSubscriptionByIdQuery(subscriptionId);
        var subscription = await subscriptionQueryService.Handle(getSubscriptionByIdQuery);
        await subscriptionCommandService.Delete(subscription);
        return Ok(subscription);
    }
    
    [HttpPut("{subscriptionId:int}")]
[SwaggerOperation(
        Summary = "Updates a Subscription by id",
        Description = "Updates a Subscription for a given identifier",
        OperationId = "UpdateSubscriptionById")]
    [SwaggerResponse(200, "The Subscription was found", typeof(SubscriptionResource))]
    public async Task<IActionResult> UpdateSubscriptionById(int subscriptionId, [FromBody] SubscriptionResource subscriptionResource)
    {
        var getSubscriptionByIdQuery = new GetSubscriptionByIdQuery(subscriptionId);
        var subscription = await subscriptionQueryService.Handle(getSubscriptionByIdQuery);
        subscription.Name = subscriptionResource.Name;
        subscription.MonthlyPrice = subscriptionResource.MonthlyPrice;
        await subscriptionCommandService.Update(subscription);
        return Ok(subscription);
    }
}