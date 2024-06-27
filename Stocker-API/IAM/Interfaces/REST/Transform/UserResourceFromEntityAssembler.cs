using Stocker_API.IAM.Domain.Model.Aggregates;
using Stocker_API.IAM.Interfaces.REST.Resources;

namespace Stocker_API.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(user.Id, user.Username);
    }
}