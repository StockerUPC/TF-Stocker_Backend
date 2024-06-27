using System.Net.Mime;
using Stocker_API.Profiles.Domain.Model.Queries;
using Stocker_API.Profiles.Domain.Services;
using Stocker_API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace Stocker_API.Profiles.Interfaces.REST;

[ApiController]
[Route("/api/v1/subscriptions/{subscriptionId}/profiles")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Subscriptions")]
public class SubscriptionProfilesController(IProfileQueryService profileQueryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProfilesBySubscriptionId([FromRoute] int subscriptionId)
    {
        var getAllProfilesBySubscriptionIdQuery = new GetAllProfilesBySubscriptionIdQuery(subscriptionId);
        var profiles = await profileQueryService.Handle(getAllProfilesBySubscriptionIdQuery);
        var resources = profiles.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}