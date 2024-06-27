using Stocker_API.IAM.Domain.Model.Commands;
using Stocker_API.IAM.Interfaces.REST.Resources;

namespace Stocker_API.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    }
}