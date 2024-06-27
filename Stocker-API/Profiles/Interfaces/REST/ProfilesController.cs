using System.Net.Mime;
using Stocker_API.Profiles.Domain.Model.Queries;
using Stocker_API.Profiles.Domain.Services;
using Stocker_API.Profiles.Interfaces.REST.Resources;
using Stocker_API.Profiles.Interfaces.REST.Transform;
using Stocker_API.Profiles.Domain.Model.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Stocker_API.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ProfilesController(IProfileCommandService profileCommandService, IProfileQueryService profileQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProfile(CreateProfileResource resource)
    {
        var createProfileCommand = CreateProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var profile = await profileCommandService.Handle(createProfileCommand);
        if (profile is null) return BadRequest();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return CreatedAtAction(nameof(GetProfileById), new {profileId = profileResource.Id}, profileResource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllProfiles()
    {
        var getAllProfilesQuery = new GetAllProfilesQuery();
        var profiles = await profileQueryService.Handle(getAllProfilesQuery);
        var profileResources = profiles.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(profileResources);
    }
    
    [HttpGet("{profileId:int}")]
    public async Task<IActionResult> GetProfileById(int profileId)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        if (profile == null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }
    
    [HttpDelete("{profileId:int}")]
    [SwaggerOperation(
        Summary = "Deletes a profile by id",
        Description = "Deletes a profile for a given identifier",
        OperationId = "DeleteProfileById")]
    [SwaggerResponse(200, "The profile was deleted", typeof(ProfileResource))]
    public async Task<IActionResult> DeleteProfileById(int profileId)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        await profileCommandService.Delete(profile);
        return Ok(profile);
    }
    
    [HttpPut("{profileId:int}")]
    [SwaggerOperation(
        Summary = "Updates a profile by id",
        Description = "Updates a profile for a given identifier",
        OperationId = "UpdateProfileById")]
    [SwaggerResponse(200, "The profile was updated", typeof(ProfileResource))]
    public async Task<IActionResult> UpdateProfileById(int profileId, [FromBody] ProfileResource profileResource)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        var nameParts = profileResource.FullName.Split(' ');
        profile.Name = new PersonName(nameParts[0], nameParts.Length > 1 ? nameParts[1] : string.Empty);
        profile.Email = new EmailAddress(profileResource.Email);
        profile.Address = new StreetAddress(profileResource.StreetAddress, "", "", "", "");
        await profileCommandService.Update(profile);
        return Ok(profile);
    }
    
    
    
    
}