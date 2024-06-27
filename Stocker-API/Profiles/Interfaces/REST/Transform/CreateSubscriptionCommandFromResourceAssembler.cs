using Stocker_API.Profiles.Domain.Model.Commands;
using Stocker_API.Profiles.Interfaces.REST.Resources;

namespace Stocker_API.Profiles.Interfaces.REST.Transform;

public static class CreateSubscriptionCommandFromResourceAssembler
{
    public static CreateSubscriptionCommand ToCommandFromResource(CreateSubscriptionResource resource)
    {
        return new CreateSubscriptionCommand(resource.Name, resource.MonthlyPrice);
    }
}